using GymMGMT.Application.Responses;
using GymMGMT.Application.Security.Contracts;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ICommandResponse>
    {
        private readonly IAuthenticationService _authenticationService;

        public CreateUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<ICommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _authenticationService.CreateUserAsync(request.Email, request.Password);

            return new CommandResponse(userId);
        }
    }
}
