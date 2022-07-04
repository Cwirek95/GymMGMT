using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingsList
{
    public class GetTrainingsListQuery : IRequest<IEnumerable<TrainingsInListViewModel>>, ICacheable
    {
        public string CacheKey => $"GetTrainingsList";
    }
}