using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Application.Responses;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Trainers.Commands.AddTrainer
{
    public class AddTrainerCommandHandler : IRequestHandler<AddTrainerCommand, ICommandResponse>
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddTrainerCommandHandler(ITrainerRepository trainerRepository,
            IMapper mapper, IUserRepository userRepository)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ICommandResponse> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            var existTrainer = await _trainerRepository.GetByUserIdWithDetailsAsync(request.UserId);
            if (existTrainer != null)
                throw new ConflictException("There is already a trainer assigned to this user");

            var trainer = _mapper.Map<Trainer>(request);

            trainer = await _trainerRepository.AddAsync(trainer);

            return new CommandResponse(trainer.Id);
        }
    }
}