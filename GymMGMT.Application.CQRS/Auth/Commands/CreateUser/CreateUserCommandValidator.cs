using FluentValidation;

namespace GymMGMT.Application.CQRS.Auth.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(256)
                .WithMessage("{PropertyName} cannot exceed 256 characters")
                .EmailAddress()
                .WithMessage("Incorrect email address format");
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MinimumLength(5)
                .WithMessage("{PropertyName} must be at least 5 characters")
                .MaximumLength(1024)
                .WithMessage("{PropertyName} cannot exceed 1024 characters")
                .Must(x => !x.Any(y => Char.IsWhiteSpace(y)))
                .WithMessage("{PropertyName} cannot contains whitespace");
        }
    }
}
