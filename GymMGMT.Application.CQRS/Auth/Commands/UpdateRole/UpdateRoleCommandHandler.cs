using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ICommandResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ICommandResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);

            role.Name = request.Name;

            await _roleRepository.UpdateAsync(role);

            return new CommandResponse();
        }
    }
}
