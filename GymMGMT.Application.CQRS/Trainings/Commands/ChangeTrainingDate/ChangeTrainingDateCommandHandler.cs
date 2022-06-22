using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingDate
{
    public class ChangeTrainingDateCommandHandler : IRequestHandler<ChangeTrainingDateCommand, ICommandResponse>
    {
        private readonly ITrainingRepository _trainingRepository;

        public ChangeTrainingDateCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeTrainingDateCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetByIdAsync(request.Id);

            training.StartDate = request.StartDate;
            training.EndDate = request.EndDate;

            await _trainingRepository.UpdateAsync(training);

            return new CommandResponse();
        }
    }
}