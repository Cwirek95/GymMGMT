using GymMGMT.Application.Models.Responses;
using GymMGMT.Application.Security.Contracts;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.SignInUser
{
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, AuthenticationResponse>
    {
        private readonly IAuthenticationService _authenticationService;

        public SignInUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<AuthenticationResponse> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.AuthenticateAsync(request.Email, request.Password);

            return new AuthenticationResponse(response.Id, response.Email, response.Token);
        }
    }
}