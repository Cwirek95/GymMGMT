namespace GymMGMT.Application.CQRS.Memberships.Queries.GetMembershipDetail
{
    public class GetMembershipDetailQuery : IRequest<MembershipDetailViewModel>
    {
        public int Id { get; set; }
    }
}