using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Trainers.Queries.GetTrainerDetail
{
    public class GetTrainerDetailQueryHandler : IRequestHandler<GetTrainerDetailQuery, TrainerDetailViewModel>
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public GetTrainerDetailQueryHandler(ITrainerRepository trainerRepository, IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }

        public async Task<TrainerDetailViewModel> Handle(GetTrainerDetailQuery request, CancellationToken cancellationToken)
        {
            var trainer = await _trainerRepository.GetByIdWithDetailsAsync(request.Id);
            if (trainer == null)
                throw new NotFoundException(nameof(Trainer), request.Id);

            return _mapper.Map<TrainerDetailViewModel>(trainer);
        }
    }
}