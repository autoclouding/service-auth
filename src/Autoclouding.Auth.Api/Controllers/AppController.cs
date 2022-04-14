namespace Autoclouding.Auth.Api.Controllers;

[Produces("application/json")]
[Route("")]
public abstract class AppController : ControllerBase
{

    [HttpGet("")]
    public RedirectResult Index()
    {
        return Redirect("/swagger");
    }
}
