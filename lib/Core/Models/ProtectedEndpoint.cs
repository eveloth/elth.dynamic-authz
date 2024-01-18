using System.Security.Claims;

namespace Core.Models;

public class ProtectedEndpoint
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string DisplayName { get; set; } = default!;
    public string HttpMethod { get; set; } = default!;
    public string Route { get; set; } = default!;
    public Claim? Policy { get; set; }
}