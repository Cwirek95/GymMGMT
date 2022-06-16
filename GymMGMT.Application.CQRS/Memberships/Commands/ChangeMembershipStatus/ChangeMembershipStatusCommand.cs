using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipStatus
{
    public class ChangeMembershipStatusCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}