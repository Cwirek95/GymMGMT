using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainers.Commands.ChangeTrainerStatus
{
    public class ChangeTrainerStatusCommandHandler : IRequestHandler<ChangeTrainerStatusCommand, ICommandResponse>
    {
        private readonly ITrainerRepository _trainerRepository;

        public ChangeTrainerStatusCommandHandler(ITrainerRepository trainerRepository)
        {
            _trainerRepository = trainerRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeTrainerStatusCommand request, CancellationToken cancellationToken)
        {
            var trainer = await _trainerRepository.GetByIdAsync(request.Id);

            trainer.Status = !trainer.Status;

            await _trainerRepository.UpdateAsync(trainer);

            return new CommandResponse();
        }
    }
}