namespace GymMGMT.Application.CQRS.Members.Commands.UpdateMember
{
    public class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
    {
        public UpdateMemberCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(256)
                .WithMessage("{PropertyName} must not exceed 128 characters");
            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(256)
                .WithMessage("{PropertyName} must not exceed 128 characters");
            RuleFor(x => x.DateOfBirth)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(256)
                .WithMessage("{PropertyName} must not exceed 15 characters")
                .Matches(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")
                .WithMessage("Wrong phone number format");
        }
    }
}