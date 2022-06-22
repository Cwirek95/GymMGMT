using GymMGMT.Application.Common;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingPrice
{
    public class ChangeTrainingPriceCommandValidator : AbstractValidator<ChangeTrainingPriceCommand>
    {
        public ChangeTrainingPriceCommandValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} cannot be less than 0")
                .Must(PriceFormat.CheckPriceFormat)
                .WithMessage("Wrong price format");
        }
    }
}