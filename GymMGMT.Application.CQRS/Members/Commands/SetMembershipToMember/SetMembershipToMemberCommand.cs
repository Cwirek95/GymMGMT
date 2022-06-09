using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Members.Commands.SetMembershipToMember
{
    public class SetMembershipToMemberCommand : IRequest<ICommandResponse>
    {
        public int MemberId { get; set; }
        public int MembershipId { get; set; }
    }
}