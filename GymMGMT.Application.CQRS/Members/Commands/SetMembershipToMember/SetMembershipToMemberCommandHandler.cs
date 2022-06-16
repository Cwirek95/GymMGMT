using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Members.Commands.SetMembershipToMember
{
    public class SetMembershipToMemberCommandHandler : IRequestHandler<SetMembershipToMemberCommand, ICommandResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMembershipRepository _membershipRepository;

        public SetMembershipToMemberCommandHandler(IMemberRepository memberRepository,
            IMembershipRepository membershipRepository)
        {
            _memberRepository = memberRepository;
            _membershipRepository = membershipRepository;
        }

        public async Task<ICommandResponse> Handle(SetMembershipToMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.MemberId);
            var membership = await _membershipRepository.GetByIdAsync(request.MembershipId);

            member.MembershipId = request.MembershipId;
            await _memberRepository.UpdateAsync(member);

            return new CommandResponse();
        }
    }
}