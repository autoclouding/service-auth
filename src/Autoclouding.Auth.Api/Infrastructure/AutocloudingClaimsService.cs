using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Autoclouding.Auth.Api.Services;

namespace Autoclouding.Auth.Api.Infrastructure;
public class AutocloudingClaimsService : JwtBearerEvents
{
    private readonly AuthHttpClient _client;
    private readonly IMemoryCache _memoryCache;

    public AutocloudingClaimsService(AuthHttpClient authHttpClient, IMemoryCache memoryCache)
    {
        _client = authHttpClient;
        _memoryCache = memoryCache;
    }

    public override async Task TokenValidated(TokenValidatedContext context)
    {
        if (context.Principal is null)
        {
            return;
        }

        var key = context.Principal.Claims.First(c => c.Type == Current.Auth.ObjectIdentifierClaimType).Value;

        var claims = await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(60);

            var bearerTokenHeader = context.HttpContext.Request.Headers["Authorization"].ToString();
            var bearerToken = bearerTokenHeader.Split(' ')[1];
            var userProfile = await _client.GetUserProfileAsync(bearerToken);

            var claims = new List<Claim>();

            foreach (var permission in userProfile.Permissions)
            {
                claims.Add(new Claim(ClaimTypes.Role, permission));
            }

            return claims;
        });

        var appIdentity = new ClaimsIdentity(claims);

        context.Principal.AddIdentity(appIdentity);
    }
}
