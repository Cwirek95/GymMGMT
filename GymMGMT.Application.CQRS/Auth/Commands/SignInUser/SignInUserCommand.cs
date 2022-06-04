using GymMGMT.Application.Models.Responses;

namespace GymMGMT.Application.CQRS.Auth.Commands.SignInUser
{
    public class SignInUserCommand : IRequest<AuthenticationResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
