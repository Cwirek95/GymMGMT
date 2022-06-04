namespace GymMGMT.Application.CQRS.Auth.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(256)
                .WithMessage("{PropertyName} must not exceed 256 characters");
        }
    }
}