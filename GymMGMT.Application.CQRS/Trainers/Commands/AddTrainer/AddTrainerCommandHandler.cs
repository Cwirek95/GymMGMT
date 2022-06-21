using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Trainers.Commands.AddTrainer
{
    public class AddTrainerCommandHandler : IRequestHandler<AddTrainerCommand, ICommandResponse>
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public AddTrainerCommandHandler(ITrainerRepository trainerRepository, IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }

        public async Task<ICommandResponse> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
        {
            var trainer = _mapper.Map<Trainer>(request);

            trainer = await _trainerRepository.AddAsync(trainer);

            return new CommandResponse(trainer.Id);
        }
    }
}