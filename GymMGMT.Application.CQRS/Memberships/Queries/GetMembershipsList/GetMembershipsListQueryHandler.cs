using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;

namespace GymMGMT.Application.CQRS.Memberships.Queries.GetMembershipsList
{
    public class GetMembershipsListQueryHandler : IRequestHandler<GetMembershipsListQuery, IEnumerable<MembershipsInListViewModel>>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;

        public GetMembershipsListQueryHandler(IMembershipRepository membershipRepository, IMapper mapper)
        {
            _membershipRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MembershipsInListViewModel>> Handle(GetMembershipsListQuery request, CancellationToken cancellationToken)
        {
            var memberships = await _membershipRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<MembershipsInListViewModel>>(memberships);
        }
    }
}