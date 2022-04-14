using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Autoclouding.Auth.Api.Services;

namespace Autoclouding.Auth.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class GithubController : ControllerBase
{
    private readonly GithubHttpClient _githubClient;
    private readonly ILogger<GithubHttpClient> _logger;


    public GithubController(GithubHttpClient githubClient, ILogger<GithubHttpClient> logger)
    {
        _githubClient = githubClient;
        _logger = logger;
    }

    // Example: Allow anonymous access to this action (override default policy)
    [AllowAnonymous]
    [HttpGet("{respositoryName}")]
    public async Task<ActionResult<GithubRepository>> Get(string respositoryName)
    {
        try
        {
            var githubLocation = await _githubClient.GetGithubRepositoryActionAsync(respositoryName);
            return Ok(githubLocation);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "some error occured");
            return BadRequest(exception.Message);
            // throw new ArgumentException();
        }
    }
}
