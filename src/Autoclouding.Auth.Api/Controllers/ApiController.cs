using Microsoft.AspNetCore.Authorization;

namespace Autoclouding.Auth.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthController : AppController
{
    public AuthController()
    {
    }

    [HttpGet("GetPlanets")]
    public ActionResult<List<Planet>> GetPlanets()
    {
        var planets = new List<Planet>()
        {
            new Planet{Id = 1, Name = "Mercury", DistanceFromSun = 0.4m},
            new Planet{Id = 2, Name = "Venus", DistanceFromSun = 0.7m},
            new Planet{Id = 3, Name = "Earth", DistanceFromSun = 1m},
            new Planet{Id = 4, Name = "Mars", DistanceFromSun = 1.5m},
            new Planet{Id = 5, Name = "Jupiter", DistanceFromSun = 5.2m},
            new Planet{Id = 6, Name = "Saturn", DistanceFromSun = 9.5m},
            new Planet{Id = 7, Name = "Uranus", DistanceFromSun = 19.2m},
            new Planet{Id = 8, Name = "Neptune", DistanceFromSun = 30.1m}
        };

        return Ok(planets);
    }

    // Example: override default authorization policy to require an imaginary "PlanetReader" role
    [Authorize(Roles = "PlanetReader")]
    [HttpGet("GetPlanet/{id}")]
    public ActionResult<Planet> GetPlanet(int id)
    {
        var planet = new Planet()
        {
            Id = id,
            Name = "Mercury",
            DistanceFromSun = 0.4m
        };

        return Ok(planet);
    }
}
