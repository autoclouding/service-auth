using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Autoclouding.Auth.Api.Services;
public class GithubHttpClient
{
    private readonly HttpClient _httpClient;

    public GithubHttpClient(HttpClient httpClient, IOptions<Config> options)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + options.Value.Github.Secret);
        _httpClient.BaseAddress = new Uri(options.Value.Github.ApiUrl);
    }

    public async Task<GithubRepository> GetGithubRepositoryActionAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<GithubRepository>("/repos/autoclouding/dev-image");
        return response ?? new GithubRepository();
    }
}
