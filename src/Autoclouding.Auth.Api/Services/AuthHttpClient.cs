using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace Autoclouding.Auth.Api.Services;
public class AuthHttpClient
{
    private const string MeUrl = "/v1.0/me";
    private readonly HttpClient _httpClient;
    private readonly Config _config;

    public AuthHttpClient(HttpClient httpClient, IOptions<Config> options)
    {
        _httpClient = httpClient;
        _config = options.Value;
        _httpClient.BaseAddress = new Uri(_config.AuthApiUrl);
    }

    public async Task<UserProfileResponse> GetUserProfileAsync(string bearerToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, MeUrl);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var userProfile = await response.Content.ReadFromJsonAsync<UserProfileResponse>();

        return userProfile!;
    }
}
