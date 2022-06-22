using GymMGMT.Application.Common;

namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.CreateMembershipType
{
    public class CreateMembershipTypeCommandValidator : AbstractValidator<CreateMembershipTypeCommand>
    {
        public CreateMembershipTypeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(128)
                .WithMessage("{PropertyName} must not exceed 128 characters");
            RuleFor(x => x.DefaultPrice)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Must(PriceFormat.CheckPriceFormat)
                .WithMessage("Wrong price format")
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0");
            RuleFor(x => x.DurationInDays)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0");
        }
    }
}