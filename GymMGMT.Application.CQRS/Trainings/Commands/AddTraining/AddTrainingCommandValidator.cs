using GymMGMT.Application.Common;

namespace GymMGMT.Application.CQRS.Trainings.Commands.AddTraining
{
    public class AddTrainingCommandValidator : AbstractValidator<AddTrainingCommand>
    {
        public AddTrainingCommandValidator()
        {
            RuleFor(x => x.StartDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .LessThan(x => x.EndDate)
                .WithMessage("{PropertyName} must be earlier than EndDate");
            RuleFor(x => x.EndDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .GreaterThan(x => x.StartDate)
                .WithMessage("{PropertyName} must be later than StartDate");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} cannot be less than 0")
                .Must(PriceFormat.CheckPriceFormat)
                .WithMessage("Wrong price format");
            RuleFor(x => x.TrainingType)
                .NotNull()
                .NotEmpty()
                .WithMessage("{ProperyName} is required");
        }
    }
}