using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.Trainers.Queries.GetTrainersList
{
    public class GetTrainersListQuery : IRequest<IEnumerable<TrainersInListViewModel>>, ICacheable
    {
        public string CacheKey => $"GetTrainersList";
    }
}