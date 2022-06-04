using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Notifications.Commands
{
    public class SendEmailCommand : IRequest<ICommandResponse>
    {
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
