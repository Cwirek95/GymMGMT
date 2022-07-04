using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.Auth.Queries.GetUsersList
{
    public class GetUsersListQuery : IRequest<IEnumerable<UsersInListViewModel>>, ICacheable
    {
        public string CacheKey => $"GetUsersList";
    }
}