using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.AddMembership
{
    public class AddMembershipCommand : IRequest<ICommandResponse>
    {
        public double? Price { get; set; }


        public int MemberId { get; set; }
        public int MembershipTypeId { get; set; }
    }
}