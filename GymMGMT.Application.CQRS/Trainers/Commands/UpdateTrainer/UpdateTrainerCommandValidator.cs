namespace GymMGMT.Application.CQRS.Trainers.Commands.UpdateTrainer
{
    public class UpdateTrainerCommandValidator : AbstractValidator<UpdateTrainerCommand>
    {
        public UpdateTrainerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(256)
                .WithMessage("{PropertyName} must not exceed 256 characters");
            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("{PropertyName} is required")
                .MaximumLength(256)
                .WithMessage("{PropertyName} must not exceed 256 characters");
        }
    }
}