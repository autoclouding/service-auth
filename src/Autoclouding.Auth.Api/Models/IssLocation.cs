using System.Runtime.InteropServices;
using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace Autoclouding.Auth.Api.Models;
public class IssLocation
{
    public string Name { get; set; } = default!;

    public int Id { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double Altitude { get; set; }

    public double Velocity { get; set; }

    public string Visibility { get; set; } = default!;

    public double Footprint { get; set; }

    public int Timestamp { get; set; }

    public double Daynum { get; set; }

    [J("Solar_lat")]
    public double SolarLatitude { get; set; }

    [J("Solar_lon")]
    public double SolarLongitude { get; set; }

    public string Units { get; set; } = default!;

}
