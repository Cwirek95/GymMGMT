using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Auth.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<ICommandResponse>
    {
        public Guid Id { get; set; }
    }
}
