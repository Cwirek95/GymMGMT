using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;

namespace GymMGMT.Application.CQRS.Auth.Queries.GetRolesList
{
    public class GetRolesListQueryHandler : IRequestHandler<GetRolesListQuery, IEnumerable<RolesInListViewModel>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public GetRolesListQueryHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RolesInListViewModel>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<RolesInListViewModel>>(roles);
        }
    }
}
