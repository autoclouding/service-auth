using System.Runtime.InteropServices;

namespace Autoclouding.Auth.Api.Models;

public class Contact
{
    public string Email { get; set; } = default!;
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Phone { get; set; } = default!;
}
