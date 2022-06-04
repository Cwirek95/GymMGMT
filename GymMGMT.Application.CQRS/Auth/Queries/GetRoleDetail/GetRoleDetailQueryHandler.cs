using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;

namespace GymMGMT.Application.CQRS.Auth.Queries.GetRoleDetail
{
    public class GetRoleDetailQueryHandler : IRequestHandler<GetRoleDetailQuery, RoleDetailViewModel>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleDetailQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleDetailViewModel> Handle(GetRoleDetailQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);

            return _mapper.Map<RoleDetailViewModel>(role);
        }
    }
}
