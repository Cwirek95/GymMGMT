using MimeKit;

namespace GymMGMT.Application.Models
{
    public class EmailMessage
    {
        private string[] strings;

        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public EmailMessage(IEnumerable<EmailAddress> to, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x.DisplayName, x.Address)));
            Subject = subject;
            Content = content;
        }
    }
}
