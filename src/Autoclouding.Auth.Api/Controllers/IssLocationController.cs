using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Autoclouding.Auth.Api.Services;

namespace Autoclouding.Auth.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class IssLocationController : ControllerBase
{
    private readonly IssHttpClient _issClient;

    public IssLocationController(IssHttpClient issClient)
    {
        _issClient = issClient;
    }

    // Example: Allow anonymous access to this action (override default policy)
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IssLocation>> Get()
    {
        var issLocation = await _issClient.GetIssLocationAsync();
        return Ok(issLocation);
    }
}
