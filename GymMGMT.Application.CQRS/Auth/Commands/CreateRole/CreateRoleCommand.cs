using GymMGMT.Application.Responses;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<ICommandResponse>
    {
        public string Name { get; set; }
    }
}
