using GymMGMT.Application.Responses;
using GymMGMT.Application.Security.Contracts;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ICommandResponse>
    {
        private readonly IAuthenticationService _authenticationService;

        public DeleteUserCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<ICommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _authenticationService.DeleteUserAsync(request.Id);

            return new CommandResponse();
        }
    }
}