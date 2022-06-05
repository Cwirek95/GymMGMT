namespace GymMGMT.Application.CQRS.Members.Queries.GetMemberDetail
{
    public class MembershipDto
    {
        public string MembershipTypeName { get; set; }
        public double Price { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset LastExtension { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool Status { get; set; }
    }
}