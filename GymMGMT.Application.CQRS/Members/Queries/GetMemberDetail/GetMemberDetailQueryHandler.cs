using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;

namespace GymMGMT.Application.CQRS.Members.Queries.GetMemberDetail
{
    public class GetMemberDetailQueryHandler : IRequestHandler<GetMemberDetailQuery, MemberDetailViewModel>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetMemberDetailQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<MemberDetailViewModel> Handle(GetMemberDetailQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdWithDetailsAsync(request.Id);

            return _mapper.Map<MemberDetailViewModel>(member);
        }
    }
}