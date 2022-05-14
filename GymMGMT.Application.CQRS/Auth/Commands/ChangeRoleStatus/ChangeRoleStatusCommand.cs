using GymMGMT.Application.Responses;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.ChangeRoleStatus
{
    public class ChangeRoleStatusCommand : IRequest<ICommandResponse>
    {
        public Guid Id { get; set; }
    }
}
