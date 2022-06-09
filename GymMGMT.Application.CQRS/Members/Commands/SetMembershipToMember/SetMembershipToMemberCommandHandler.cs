using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Members.Commands.SetMembershipToMember
{
    public class SetMembershipToMemberCommandHandler : IRequestHandler<SetMembershipToMemberCommand, ICommandResponse>
    {
        private readonly IMemberRepository _memberRepository;

        public SetMembershipToMemberCommandHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<ICommandResponse> Handle(SetMembershipToMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.MemberId);

            member.MembershipId = request.MembershipId;
            await _memberRepository.UpdateAsync(member);

            return new CommandResponse();
        }
    }
}