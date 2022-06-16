using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.SetDefaultPriceForCurrentMembers
{
    public class SetDefaultPriceForCurrentMembersCommand : IRequest<ICommandResponse>
    {
        public int MembershipTypeId { get; set; }
    }
}