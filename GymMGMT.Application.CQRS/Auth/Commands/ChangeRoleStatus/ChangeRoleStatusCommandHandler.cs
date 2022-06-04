using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Auth.Commands.ChangeRoleStatus
{
    public class ChangeRoleStatusCommandHandler : IRequestHandler<ChangeRoleStatusCommand, ICommandResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public ChangeRoleStatusCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeRoleStatusCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);

            role.Status = !role.Status;

            await _roleRepository.UpdateAsync(role);

            return new CommandResponse();
        }
    }
}
