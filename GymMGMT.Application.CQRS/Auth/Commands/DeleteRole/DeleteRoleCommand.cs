using GymMGMT.Application.Responses;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest<ICommandResponse>
    {
        public Guid Id { get; set; }
    }
}
