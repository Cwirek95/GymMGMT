using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;

namespace GymMGMT.Application.CQRS.MembershipTypes.Queries.GetMembershipTypesList
{
    public class GetMembershipTypesListQueryHandler : IRequestHandler<GetMembershipTypesListQuery, IEnumerable<MembershipTypesInListViewModel>>
    {
        private readonly IMembershipTypeRepository _membershipTypeRepository;
        private readonly IMapper _mapper;

        public GetMembershipTypesListQueryHandler(IMembershipTypeRepository membershipTypeRepository, IMapper mapper)
        {
            _membershipTypeRepository = membershipTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MembershipTypesInListViewModel>> Handle(GetMembershipTypesListQuery request, CancellationToken cancellationToken)
        {
            var membershipTypes = await _membershipTypeRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<MembershipTypesInListViewModel>>(membershipTypes);
        }
    }
}