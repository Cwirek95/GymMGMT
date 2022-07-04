using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.MembershipTypes.Queries.GetMembershipTypeDetail
{
    public class GetMembershipTypeDetailQuery : IRequest<MembershipTypeDetailViewModel>, ICacheable
    {
        public int Id { get; set; }

        public string CacheKey => $"GetMembershipTypeDetail-{Id}";
    }
}