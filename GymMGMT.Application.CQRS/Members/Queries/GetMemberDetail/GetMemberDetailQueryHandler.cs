using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Members.Queries.GetMemberDetail
{
    public class GetMemberDetailQueryHandler : IRequestHandler<GetMemberDetailQuery, MemberDetailViewModel>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;

        public GetMemberDetailQueryHandler(IMemberRepository memberRepository,
            IMapper mapper, IMembershipRepository membershipRepository)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _membershipRepository = membershipRepository;
        }

        public async Task<MemberDetailViewModel> Handle(GetMemberDetailQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdWithDetailsAsync(request.Id);
            if (member == null)
                throw new NotFoundException(nameof(Member), request.Id);

            var membershipType = await _membershipRepository.GetByIdWithDetailsAsync((int)member.MembershipId);
            if(membershipType == null)
                throw new NotFoundException(nameof(Membership), request.Id);

            member.Membership.MembershipType.Name = membershipType.MembershipType.Name;

            return _mapper.Map<MemberDetailViewModel>(member);
        }
    }
}