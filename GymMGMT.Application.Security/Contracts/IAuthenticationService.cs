using GymMGMT.Application.Models.Responses;

namespace GymMGMT.Application.Security.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(string storageName, string password);
    }
}
