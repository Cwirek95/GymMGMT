using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingStatus
{
    public class ChangeTrainingStatusCommandHandler : IRequestHandler<ChangeTrainingStatusCommand, ICommandResponse>
    {
        private readonly ITrainingRepository _trainingRepository;

        public ChangeTrainingStatusCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeTrainingStatusCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetByIdAsync(request.Id);

            training.Status = !training.Status;

            await _trainingRepository.UpdateAsync(training);

            return new CommandResponse();
        }
    }
}