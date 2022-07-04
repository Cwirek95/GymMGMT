using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.Memberships.Queries.GetMembershipDetail
{
    public class GetMembershipDetailQuery : IRequest<MembershipDetailViewModel>, ICacheable
    {
        public int Id { get; set; }

        public string CacheKey => $"GetMembershipDetail-{Id}";
    }
}