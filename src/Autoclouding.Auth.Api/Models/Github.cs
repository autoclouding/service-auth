using System.Runtime.InteropServices;
using J = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace Autoclouding.Auth.Api.Models;
public class GithubRepository
{
    public string Name { get; set; } = default!;

    public int Id { get; set; }
}
