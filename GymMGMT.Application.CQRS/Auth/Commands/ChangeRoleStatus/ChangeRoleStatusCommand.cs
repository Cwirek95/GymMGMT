using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Auth.Commands.ChangeRoleStatus
{
    public class ChangeRoleStatusCommand : IRequest<ICommandResponse>
    {
        public Guid Id { get; set; }
    }
}
