using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Exceptions;
using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingDetail
{
    public class GetTrainingDetailQueryHandler : IRequestHandler<GetTrainingDetailQuery, TrainingDetailViewModel>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public GetTrainingDetailQueryHandler(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<TrainingDetailViewModel> Handle(GetTrainingDetailQuery request, CancellationToken cancellationToken)
        {
            var trainer = await _trainingRepository.GetByIdWithDetailsAsync(request.Id);
            if (trainer == null)
                throw new NotFoundException(nameof(Training), request.Id);

            return _mapper.Map<TrainingDetailViewModel>(trainer);
        }
    }
}