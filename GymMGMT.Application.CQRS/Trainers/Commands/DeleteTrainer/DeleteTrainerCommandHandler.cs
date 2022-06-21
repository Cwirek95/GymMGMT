using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainers.Commands.DeleteTrainer
{
    public class DeleteTrainerCommandHandler : IRequestHandler<DeleteTrainerCommand, ICommandResponse>
    {
        private readonly ITrainerRepository _trainerRepository;

        public DeleteTrainerCommandHandler(ITrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }

        public async Task<ICommandResponse> Handle(DeleteTrainerCommand request, CancellationToken cancellationToken)
        {
            var trainer = await _trainerRepository.GetByIdAsync(request.Id);

            await _trainerRepository.DeleteAsync(trainer);

            return new CommandResponse();
        }
    }
}