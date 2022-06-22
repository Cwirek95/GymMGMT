using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingType
{
    public class ChangeTrainingTypeCommandHandler : IRequestHandler<ChangeTrainingTypeCommand, ICommandResponse>
    {
        private readonly ITrainingRepository _trainingRepository;

        public ChangeTrainingTypeCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeTrainingTypeCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetByIdAsync(request.Id);

            if (training.TrainingType == Domain.Enums.TrainingType.INDIVIDUAL)
            {
                training.TrainingType = Domain.Enums.TrainingType.GROUP;
            } 
            else
            {
                training.TrainingType = Domain.Enums.TrainingType.INDIVIDUAL;
            }

            await _trainingRepository.UpdateAsync(training);

            return new CommandResponse();
        }
    }
}