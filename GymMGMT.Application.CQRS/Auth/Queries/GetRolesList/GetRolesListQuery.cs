using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.Auth.Queries.GetRolesList
{
    public class GetRolesListQuery : IRequest<IEnumerable<RolesInListViewModel>>, ICacheable
    {
        public string CacheKey => $"GetRolesList";
    }
}
