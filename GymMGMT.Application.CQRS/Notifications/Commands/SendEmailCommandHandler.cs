using GymMGMT.Application.Contracts.Services;
using GymMGMT.Application.Models;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Notifications.Commands
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, ICommandResponse>
    {
        private readonly IEmailSenderService _emailSender;

        public SendEmailCommandHandler(IEmailSenderService emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task<ICommandResponse> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var emailAddress = new EmailAddress()
            { 
                DisplayName = request.Receiver,
                Address = request.Receiver
            };
            var message = new EmailMessage(
                new List<EmailAddress>() { emailAddress },
                request.Subject,
                request.Content);

            await _emailSender.SendEmailAsync(message);

            return new CommandResponse();
        }
    }
}
