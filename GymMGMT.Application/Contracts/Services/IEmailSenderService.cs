using GymMGMT.Application.Models;

namespace GymMGMT.Application.Contracts.Services
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
