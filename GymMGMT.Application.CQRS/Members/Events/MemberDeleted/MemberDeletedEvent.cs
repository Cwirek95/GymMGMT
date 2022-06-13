namespace GymMGMT.Application.CQRS.Members.Events.MemberDeleted
{
    public class MemberDeletedEvent : INotification
    {
        public int MemberId { get; set; }
        public int MembershipId { get; set; }

        public MemberDeletedEvent(int memberId, int membershipId)
        {
            MemberId = memberId;
            MembershipId = membershipId;
        }
    }
}