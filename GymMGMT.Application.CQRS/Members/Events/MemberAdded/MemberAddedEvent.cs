namespace GymMGMT.Application.CQRS.Members.Events.MemberAdded
{
    public class MemberAddedEvent : INotification
    {
        public int MemberId { get; set; }
        public int MembershipTypeId { get; set; }
        public double? Price { get; set; }

        public MemberAddedEvent(int memberId, int membershipTypeId, double? price)
        {
            MemberId = memberId;
            MembershipTypeId = membershipTypeId;
            Price = price;
        }
    }
}