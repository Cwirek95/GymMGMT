using AutoMapper;
using GymMGMT.Application.Contracts.Repositories;

namespace GymMGMT.Application.CQRS.Trainers.Queries.GetTrainersList
{
    public class GetTrainersListQueryHandler : IRequestHandler<GetTrainersListQuery, IEnumerable<TrainersInListViewModel>>
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public GetTrainersListQueryHandler(ITrainerRepository trainerRepository, IMapper mapper)
        {
            _trainerRepository = trainerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrainersInListViewModel>> Handle(GetTrainersListQuery request, CancellationToken cancellationToken)
        {
            var trainers = await _trainerRepository.GetAllWithDetailsAsync();

            return _mapper.Map<IEnumerable<TrainersInListViewModel>>(trainers);
        }
    }
}