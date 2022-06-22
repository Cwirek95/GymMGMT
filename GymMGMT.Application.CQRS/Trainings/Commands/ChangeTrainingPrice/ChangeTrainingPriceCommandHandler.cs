using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingPrice
{
    public class ChangeTrainingPriceCommandHandler : IRequestHandler<ChangeTrainingPriceCommand, ICommandResponse>
    {
        private readonly ITrainingRepository _trainingRepository;

        public ChangeTrainingPriceCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeTrainingPriceCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetByIdAsync(request.Id);

            if (DateTimeOffset.Compare(training.EndDate, DateTimeOffset.Now) < 0)
                throw new UnprocessableEntityException("Validation failure", "The Training has already taken place");

            training.Price = request.Price;

            await _trainingRepository.UpdateAsync(training);

            return new CommandResponse();
        }
    }
}