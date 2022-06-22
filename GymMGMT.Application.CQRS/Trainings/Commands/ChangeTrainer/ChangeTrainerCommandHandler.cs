using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainer
{
    public class ChangeTrainerCommandHandler : IRequestHandler<ChangeTrainerCommand, ICommandResponse>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ITrainerRepository _trainerRepository;

        public ChangeTrainerCommandHandler(ITrainingRepository trainingRepository, ITrainerRepository trainerRepository)
        {
            _trainingRepository = trainingRepository;
            _trainerRepository = trainerRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeTrainerCommand request, CancellationToken cancellationToken)
        {
            var trainer = await _trainerRepository.GetByIdAsync(request.NewTrainerId);
            var training = await _trainingRepository.GetByIdAsync(request.Id);

            training.TrainerId = request.NewTrainerId;

            await _trainingRepository.UpdateAsync(training);

            return new CommandResponse();
        }
    }
}