namespace GymMGMT.Application.CQRS.Members.Queries.GetMembersList
{
    public class MembersInListViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public bool MembershipStatus { get; set; }
        public bool Status { get; set; }
    }
}