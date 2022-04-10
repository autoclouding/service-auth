using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Autoclouding.Auth.Api.Services;
public class IssHttpClient
{
    private readonly HttpClient _httpClient;

    public IssHttpClient(HttpClient httpClient, IOptions<Config> options)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(options.Value.IssLocationApiUrl);
    }

    public async Task<IssLocation> GetIssLocationAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<IssLocation>("v1/satellites/25544");
        return response ?? new IssLocation();
    }
}
