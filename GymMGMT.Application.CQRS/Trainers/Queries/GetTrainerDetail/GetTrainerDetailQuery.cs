using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.Trainers.Queries.GetTrainerDetail
{
    public class GetTrainerDetailQuery : IRequest<TrainerDetailViewModel>, ICacheable
    {
        public int Id { get; set; }

        public string CacheKey => $"GetTrainerDetail-{Id}";
    }
}