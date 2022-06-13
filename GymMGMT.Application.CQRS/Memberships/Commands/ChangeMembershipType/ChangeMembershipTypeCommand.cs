using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipType
{
    public class ChangeMembershipTypeCommand : IRequest<ICommandResponse>
    {
        public int MemberId { get; set; }
        public int NewMembershipTypeId { get; set; }
    }
}