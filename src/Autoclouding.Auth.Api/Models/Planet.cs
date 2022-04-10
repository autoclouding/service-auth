namespace Autoclouding.Auth.Api.Models;
public class Planet
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal DistanceFromSun { get; set; }
}
