using FluentValidation;

namespace GymMGMT.Application.CQRS.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName is required")
                .MinimumLength(5)
                .WithMessage("{PropertyName} must be at least 5 characters")
                .MaximumLength(1024)
                .WithMessage("{PropertyName} cannot exceed 1024 characters");

            RuleFor(x => x.NewPassword)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName is required")
                .MinimumLength(5)
                .WithMessage("{PropertyName} must be at least 5 characters")
                .MaximumLength(1024)
                .WithMessage("{PropertyName} cannot exceed 1024 characters");
        }
    }
}
