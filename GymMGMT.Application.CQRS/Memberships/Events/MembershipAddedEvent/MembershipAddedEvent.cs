namespace GymMGMT.Application.CQRS.Memberships.Events.MembershipAddedEvent
{
    public class MembershipAddedEvent : INotification
    {
        public int MemberId { get; set; }
        public int MembershipId { get; set; }

        public MembershipAddedEvent(int memberId, int membershipId)
        {
            MemberId = memberId;
            MembershipId = membershipId;
        }
    }
}