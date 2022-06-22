using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Responses;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Trainings.Commands.AddTraining
{
    public class AddTrainingCommandHandler : IRequestHandler<AddTrainingCommand, ICommandResponse>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public AddTrainingCommandHandler(ITrainingRepository trainingRepository, IMapper mapper,
            ITrainerRepository trainerRepository)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
            _trainerRepository = trainerRepository;
        }

        public async Task<ICommandResponse> Handle(AddTrainingCommand request, CancellationToken cancellationToken)
        {
            var trainer = await _trainerRepository.GetByIdAsync(request.TrainerId);
            var training = _mapper.Map<Training>(request);

            training = await _trainingRepository.AddAsync(training);

            return new CommandResponse(training.Id);
        }
    }
}