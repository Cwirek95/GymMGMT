namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingDate
{
    public class ChangeTrainingDateCommandValidator : AbstractValidator<ChangeTrainingDateCommand>
    {
        public ChangeTrainingDateCommandValidator()
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
        }
    }
}