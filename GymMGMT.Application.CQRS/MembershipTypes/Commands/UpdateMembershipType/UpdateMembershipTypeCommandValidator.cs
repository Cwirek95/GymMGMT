namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.UpdateMembershipType
{
    public class UpdateMembershipTypeCommandValidator : AbstractValidator<UpdateMembershipTypeCommand>
    {
        public UpdateMembershipTypeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(128)
                .WithMessage("{PropertyName} must not exceed 128 characters");
        }
    }
}
