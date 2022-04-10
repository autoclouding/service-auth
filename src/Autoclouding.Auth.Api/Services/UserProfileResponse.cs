namespace Autoclouding.Auth.Api.Services;
public record UserProfileResponse
{
    public int Id { get; set; }
    public Guid ObjectId { get; set; }
    public Guid TenantId { get; set; }
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public bool Active { get; set; }
    public DateTime? ActiveAt { get; set; }
    public HashSet<string> Permissions { get; set; } = new();
}
