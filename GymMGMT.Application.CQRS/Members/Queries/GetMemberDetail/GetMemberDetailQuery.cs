using GymMGMT.Application.Caching;

namespace GymMGMT.Application.CQRS.Members.Queries.GetMemberDetail
{
    public class GetMemberDetailQuery : IRequest<MemberDetailViewModel>, ICacheable
    {
        public int Id { get; set; }

        public string CacheKey => $"GetMemberDetail-{Id}";
    }
}