namespace GymMGMT.Application.CQRS.Notifications.Commands
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(x => x.Receiver)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(256)
                .WithMessage("{PropertyName} cannot exceed 256 characters")
                .EmailAddress()
                .WithMessage("Wrong email address format");

            RuleFor(x => x.Subject)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(256)
                .WithMessage("{PropertyName} cannot exceed 256 characters");

            RuleFor(x => x.Content)
                .MaximumLength(10000)
                .WithMessage("{PropertyName} cannot exceed 10000 characters");
        }
    }
}
