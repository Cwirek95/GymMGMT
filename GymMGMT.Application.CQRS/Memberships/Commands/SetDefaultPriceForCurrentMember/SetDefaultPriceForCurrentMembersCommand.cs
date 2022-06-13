using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.SetDefaultPriceForCurrentMember
{
    public class SetDefaultPriceForCurrentMembersCommand : IRequest<ICommandResponse>
    {
        public int MembershipTypeId { get; set; }
    }
}