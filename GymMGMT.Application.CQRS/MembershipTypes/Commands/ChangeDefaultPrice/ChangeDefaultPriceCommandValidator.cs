using GymMGMT.Application.Common;

namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.ChangeDefaultPrice
{
    public class ChangeDefaultPriceCommandValidator : AbstractValidator<ChangeDefaultPriceCommand>
    {
        public ChangeDefaultPriceCommandValidator()
        {
            RuleFor(x => x.DefaultPrice)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .Must(PriceFormat.CheckPriceFormat)
                .WithMessage("Wrong price format")
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0");
        }
    }
}