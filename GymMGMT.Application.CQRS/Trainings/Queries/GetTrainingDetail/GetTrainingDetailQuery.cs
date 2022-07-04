using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingDetail
{
    public class GetTrainingDetailQuery : IRequest<TrainingDetailViewModel>, ICacheable
    {
        public int Id { get; set; }

        public string CacheKey => $"GetTrainingDetail-{Id}";
    }
}