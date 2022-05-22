using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;
using GymMGMT.Application.Security.Contracts;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.ChangeUserRole
{
    public class ChangeUserRoleCommandHandler : IRequestHandler<ChangeUserRoleCommand, ICommandResponse>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;

        public ChangeUserRoleCommandHandler(IAuthenticationService authenticationService, IUserRepository userRepository)
        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            await _authenticationService.ChangeUserRoleAsync(user.Id, request.RoleId);

            return new CommandResponse();
        }
    }
}