using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.Members.Queries.GetMembersList
{
    public class GetMembersListQuery : IRequest<IEnumerable<MembersInListViewModel>>, ICacheable
    {
        public string CacheKey => $"GetMembersList";
    }
}