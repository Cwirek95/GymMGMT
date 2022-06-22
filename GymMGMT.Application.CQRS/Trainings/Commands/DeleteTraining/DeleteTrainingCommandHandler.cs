using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.DeleteTraining
{
    public class DeleteTrainingCommandHandler : IRequestHandler<DeleteTrainingCommand, ICommandResponse>
    {
        private readonly ITrainingRepository _trainingRepository;

        public DeleteTrainingCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<ICommandResponse> Handle(DeleteTrainingCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetByIdAsync(request.Id);

            await _trainingRepository.DeleteAsync(training);

            return new CommandResponse();
        }
    }
}