using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.MembershipTypes.Queries.GetMembershipTypesList
{
    public class GetMembershipTypesListQuery : IRequest<IEnumerable<MembershipTypesInListViewModel>>, ICacheable
    {
        public string CacheKey => $"GetMembershipTypesList";
    }
}