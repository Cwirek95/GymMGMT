using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;
using GymMGMT.Domain.Entities;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ICommandResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ICommandResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<Role>(request);
            role = await _roleRepository.AddAsync(role);

            return new CommandResponse(role.Id);
        }
    }
}
