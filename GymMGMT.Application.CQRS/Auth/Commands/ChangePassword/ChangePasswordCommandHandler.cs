using GymMGMT.Application.Responses;
using GymMGMT.Application.Security.Contracts;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ICommandResponse>
    {
        private readonly IAuthenticationService _authenticationService;

        public ChangePasswordCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<ICommandResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await _authenticationService.ChangeUserPasswordAsync(request.Id, request.OldPassword, request.NewPassword);

            return new CommandResponse();
        }
    }
}
