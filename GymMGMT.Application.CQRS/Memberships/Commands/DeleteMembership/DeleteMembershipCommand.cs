using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.DeleteMembership
{
    public class DeleteMembershipCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}