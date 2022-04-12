namespace Autoclouding.Auth.Api;
public class Config
{
    public ConnectionStrings ConnectionStrings { get; set; } = new();
    public string AppName { get; set; } = default!;
    public string AppVersion { get; set; } = default!;
    public AzureAd AzureAd { get; set; } = new();
    public string AuthApiUrl { get; set; } = default!;
    public string IssLocationApiUrl { get; set; } = default!;

    public Github Github { get; set; } = new();
}

public class Github
{
    public string ApiUrl { get; set; } = default!;
    public string Secret { get; set; } = default!;
}
public class AzureAd
{
    public string Instance { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string TenantId { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string Scopes { get; set; } = default!;
    public string CallbackPath { get; set; } = default!;
}

public class ConnectionStrings
{
    public string AuthConnectionString { get; set; } = default!;
}
