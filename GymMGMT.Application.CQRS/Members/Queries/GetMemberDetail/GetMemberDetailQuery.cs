namespace GymMGMT.Application.CQRS.Members.Queries.GetMemberDetail
{
    public class GetMemberDetailQuery : IRequest<MemberDetailViewModel>
    {
        public int Id { get; set; }
    }
}