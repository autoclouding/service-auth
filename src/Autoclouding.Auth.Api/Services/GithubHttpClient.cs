using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace Autoclouding.Auth.Api.Services;
public class GithubHttpClient
{
    private readonly HttpClient _httpClient;

    public GithubHttpClient(HttpClient httpClient, IOptions<Config> options)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        // var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("guest:" + options.Value.Github.Secret);
        // var encodedAuth = System.Convert.ToBase64String(plainTextBytes);

        var encodedAuth = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes("ddpana" + ":" + options.Value.Github.Secret));
        // var authString = $"guest:{options.Value.Github.Secret}";
        // var encodedAuth = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authString));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedAuth);

        var productValue = new ProductInfoHeaderValue("ScraperBot", "1.0");
        var commentValue = new ProductInfoHeaderValue("(+http://www.example.com/ScraperBot.html)");

        _httpClient.DefaultRequestHeaders.UserAgent.Add(productValue);
        _httpClient.DefaultRequestHeaders.UserAgent.Add(commentValue);

        _httpClient.BaseAddress = new Uri(options.Value.Github.ApiUrl);
    }

    public async Task<GithubRepository> GetGithubRepositoryActionAsync(string repositoryName)
    {
        var response = await _httpClient.GetFromJsonAsync<GithubRepository>($"/repos/autoclouding/{repositoryName}");
        return response ?? new GithubRepository();
    }
}
