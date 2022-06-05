namespace GymMGMT.Application.CQRS.Members.Commands.AddMember
{
    public class AddMemberCommandValidator : AbstractValidator<AddMemberCommand>
    {
        public AddMemberCommandValidator()
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
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
            RuleFor(x => x.MembershipId)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}