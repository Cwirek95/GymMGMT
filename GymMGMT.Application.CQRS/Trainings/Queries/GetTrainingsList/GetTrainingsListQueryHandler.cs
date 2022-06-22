using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;

namespace GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingsList
{
    public class GetTrainingsListQueryHandler : IRequestHandler<GetTrainingsListQuery, IEnumerable<TrainingsInListViewModel>>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;

        public GetTrainingsListQueryHandler(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainingsInListViewModel>> Handle(GetTrainingsListQuery request, CancellationToken cancellationToken)
        {
            var trainings = await _trainingRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<TrainingsInListViewModel>>(trainings);
        }
    }
}