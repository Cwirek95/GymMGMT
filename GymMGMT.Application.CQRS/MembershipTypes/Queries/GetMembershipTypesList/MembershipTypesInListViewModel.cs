namespace GymMGMT.Application.CQRS.MembershipTypes.Queries.GetMembershipTypesList
{
    public class MembershipTypesInListViewModel
    {
        public string Name { get; set; }
        public double DefaultPrice { get; set; }
        public int DurationInDays { get; set; }
        public bool Status { get; set; }
    }
}