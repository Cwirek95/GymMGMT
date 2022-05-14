using GymMGMT.Application.Responses;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<ICommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
