using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.ChangeMembershipTypeStatus
{
    public class ChangeMembershipTypeStatusCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}