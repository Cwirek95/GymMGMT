using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.Auth.Queries.GetRoleDetail
{
    public class GetRoleDetailQuery : IRequest<RoleDetailViewModel>, ICacheable
    {
        public Guid Id { get; set; }

        public string CacheKey => $"GetRoleDetail-{Id}";
    }
}