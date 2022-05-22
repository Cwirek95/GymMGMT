using GymMGMT.Application.Responses;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.ChangeUserRole
{
    public class ChangeUserRoleCommand : IRequest<ICommandResponse>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
