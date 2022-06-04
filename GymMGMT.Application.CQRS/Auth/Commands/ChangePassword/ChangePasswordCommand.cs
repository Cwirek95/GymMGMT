using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ICommandResponse>
    {
        public Guid Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
