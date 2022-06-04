using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Auth.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<ICommandResponse>
    {
        public string Name { get; set; }
    }
}
