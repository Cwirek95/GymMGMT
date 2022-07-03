using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;
using GymMGMT.Application.Security.Contracts;

namespace GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingsList
{
    public class GetTrainingsListQueryHandler : IRequestHandler<GetTrainingsListQuery, IEnumerable<TrainingsInListViewModel>>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetTrainingsListQueryHandler(ITrainingRepository trainingRepository,
            IMapper mapper, ICurrentUserService currentUserService)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<TrainingsInListViewModel>> Handle(GetTrainingsListQuery request, CancellationToken cancellationToken)
        {
            var trainings = await _trainingRepository.GetAllWithDetailsAsync();

            if(!_currentUserService.Role.Equals("Admin"))
                trainings = trainings.Where(x => x.CreatedBy.Equals(_currentUserService.UserId, StringComparison.OrdinalIgnoreCase)).ToList();

            return _mapper.Map<IEnumerable<TrainingsInListViewModel>>(trainings);
        }
    }
}