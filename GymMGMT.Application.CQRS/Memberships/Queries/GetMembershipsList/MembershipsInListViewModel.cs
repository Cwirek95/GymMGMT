namespace GymMGMT.Application.CQRS.Memberships.Queries.GetMembershipsList
{
    public class MembershipsInListViewModel
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset LastExtension { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
    }
}