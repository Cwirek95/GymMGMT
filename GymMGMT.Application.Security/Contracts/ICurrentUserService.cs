using System.Security.Claims;

namespace GymMGMT.Application.Security.Contracts
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal User { get; }
        string? UserId { get; }
        string? Email { get; }
        string? Role { get; }
    }
}
