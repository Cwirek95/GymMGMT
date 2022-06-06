namespace GymMGMT.Application.CQRS.MembershipTypes.Queries.GetMembershipTypeDetail
{
    public class GetMembershipTypeDetailQuery : IRequest<MembershipTypeDetailViewModel>
    {
        public int Id { get; set; }
    }
}