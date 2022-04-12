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

    public GithubController(GithubHttpClient githubClient)
    {
        _githubClient = githubClient;
    }

    // Example: Allow anonymous access to this action (override default policy)
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<GithubRepository>> Get()
    {
        var githubLocation = await _githubClient.GetGithubRepositoryActionAsync();
        return Ok(githubLocation);
    }
}
