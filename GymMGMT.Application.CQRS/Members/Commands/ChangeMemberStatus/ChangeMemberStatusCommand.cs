using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Members.Commands.ChangeMemberStatus
{
    public class ChangeMemberStatusCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}