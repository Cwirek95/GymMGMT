using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;

namespace GymMGMT.Application.CQRS.Members.Queries.GetMembersList
{
    public class GetMembersListQueryHandler : IRequestHandler<GetMembersListQuery, IEnumerable<MembersInListViewModel>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetMembersListQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MembersInListViewModel>> Handle(GetMembersListQuery request, CancellationToken cancellationToken)
        {
            var members = await _memberRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<MembersInListViewModel>>(members);
        }
    }
}