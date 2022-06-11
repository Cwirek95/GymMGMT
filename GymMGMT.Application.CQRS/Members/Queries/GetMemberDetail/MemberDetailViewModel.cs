namespace GymMGMT.Application.CQRS.Members.Queries.GetMemberDetail
{
    public class MemberDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public MembershipDto Membership { get; set; }
        public bool Status { get; set; }
    }
}