using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.ExtendMembership
{
    public class ExtendMembershipCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}