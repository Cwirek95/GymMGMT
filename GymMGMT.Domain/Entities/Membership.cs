using GymMGMT.Domain.Common;

namespace GymMGMT.Domain.Entities
{
    public class Membership : AuditableEntity, IEntity<int>
    {
        public int Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset LastExtension { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool Status { get; set; }


        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int MembershipTypeId { get; set; }
        public MembershipType MembershipType { get; set; }
    }
}