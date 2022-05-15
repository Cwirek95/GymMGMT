using GymMGMT.Application.Models.Responses;

namespace GymMGMT.Application.Security.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(string email, string password);
        Task<Guid> CreateUserAsync(string email, string password);
        Task<Guid> GetUserRoleAsync(Guid userId);
        Task<Guid> ChangeUserRoleAsync(Guid userId);
        Task DeleteUserAsync(Guid userId);
    }
}