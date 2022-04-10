using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Identity.Web;
using Autoclouding.Auth.Api.Services;

const string CorsPolicy = nameof(CorsPolicy);
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Config>(builder.Configuration);
var startupConfig = builder.Configuration.Get<Config>();

builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftAccount(microsoftOptions =>
    {
        microsoftOptions.ClientId = builder.Configuration.GetValue<string>("AzureAd:ClientId");
        microsoftOptions.ClientSecret = builder.Configuration.GetValue<string>("AzureAd:ClientSecret");
    })
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.Configure<JwtBearerOptions>("Bearer", options =>
{
    options.EventsType = typeof(AutocloudingClaimsService);
    options.TokenValidationParameters.NameClaimType = "preferred_username";
});

builder.Services.AddCorsWithCustomizations(CorsPolicy);

// Register Database Context (Postegres)
//builder.Services.AddDbContext<HelloWorldContext>(options => options.UseNpgsql(startupConfig.ConnectionStrings.HelloWorldConnectionString, x =>
//    x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
//    .EnableRetryOnFailure())
//    .UseSnakeCaseNamingConvention()
//    .ConfigureWarnings(w => w.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning))
//    .EnableSensitiveDataLogging(true));

builder.Services.AddHttpClient<IssHttpClient>();
builder.Services.AddHttpClient<AuthHttpClient>();
builder.Services.AddScoped<AutocloudingClaimsService>();

builder.Services.AddHealthChecks();

builder.Services.AddControllers();
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = false;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGenWithCustomizations(startupConfig);

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.EnablePersistAuthorization();
    o.OAuthClientId(startupConfig.AzureAd.ClientId);
    o.OAuthScopes($"{startupConfig.AzureAd.Audience}/{startupConfig.AzureAd.Scopes}");
    foreach (var description in provider.ApiVersionDescriptions)
    {
        o.SwaggerEndpoint(
        $"/swagger/{description.GroupName}/swagger.json",
        description.GroupName.ToUpperInvariant());
    }
});

app.UseHttpsRedirection();

app.UseCors(CorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/api/healthz").AllowAnonymous();

app.MapControllers().RequireAuthorization();

app.Run();
