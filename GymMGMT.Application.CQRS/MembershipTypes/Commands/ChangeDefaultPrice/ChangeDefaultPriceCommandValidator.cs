using System.Text.RegularExpressions;

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
                .Must(PriceFormat)
                .WithMessage("Wrong price format")
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than 0");
        }

        private bool PriceFormat(double price)
        {
            Regex expression = new Regex(@"^\d{0,10}(\.\d{1,2})?$");
            if (expression.IsMatch(price.ToString()))
                return true;

            return false;
        }
    }
}