using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.Auth.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<UserDetailViewModel>, ICacheable
    {
        public Guid Id { get; set; }

        public string CacheKey => $"GetUserDetail-{Id}";
    }
}