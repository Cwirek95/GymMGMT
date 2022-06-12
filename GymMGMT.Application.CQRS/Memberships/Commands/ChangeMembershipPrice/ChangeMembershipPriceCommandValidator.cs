namespace GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipPrice
{
    public class ChangeMembershipPriceCommandValidator : AbstractValidator<ChangeMembershipPriceCommand>
    {
        public ChangeMembershipPriceCommandValidator()
        {
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0");
        }
    }
}