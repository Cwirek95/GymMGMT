using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainers.Commands.UpdateTrainer
{
    public class UpdateTrainerCommandHandler : IRequestHandler<UpdateTrainerCommand, ICommandResponse>
    {
        private readonly ITrainerRepository _trainerRepository;

        public UpdateTrainerCommandHandler(ITrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }

        public async Task<ICommandResponse> Handle(UpdateTrainerCommand request, CancellationToken cancellationToken)
        {
            var trainer = await _trainerRepository.GetByIdAsync(request.Id);

            trainer.FirstName = request.FirstName;
            trainer.LastName = request.LastName;

            await _trainerRepository.UpdateAsync(trainer);

            return new CommandResponse();
        }
    }
}