namespace GymMGMT.Application.CQRS.Memberships.Queries.GetMembershipDetail
{
    public class MembershipDetailViewModel
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset LastExtension { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }


        public string MembershipTypeName { get; set; }
        public int MemberId { get; set; }
    }
}