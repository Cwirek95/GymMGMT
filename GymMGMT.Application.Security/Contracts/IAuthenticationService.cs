using GymMGMT.Application.Models.Responses;

namespace GymMGMT.Application.Security.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(string email, string password);
        Task<Guid> CreateUserAsync(string email, string password);
        Task<string> GetUserRoleAsync(Guid userId);
        Task ChangeUserPasswordAsync(Guid userId, string oldPassword, string newPassword);
        Task ChangeUserRoleAsync(Guid userId, Guid newRoleId);
        Task DeleteUserAsync(Guid userId);
    }
}