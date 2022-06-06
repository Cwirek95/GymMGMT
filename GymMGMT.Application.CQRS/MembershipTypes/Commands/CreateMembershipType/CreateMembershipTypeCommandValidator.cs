using System.Text.RegularExpressions;

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
                .Must(PriceFormat)
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

        private bool PriceFormat(double price)
        {
            Regex expression = new Regex(@"^\d{0,10}(\.\d{1,2})?$");
            if (expression.IsMatch(price.ToString()))
                return true;

            return false;
        }
    }
}