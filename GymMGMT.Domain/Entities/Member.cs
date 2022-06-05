using GymMGMT.Domain.Common;

namespace GymMGMT.Domain.Entities
{
    public class Member : AuditableEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }


        public Guid UserId { get; set; }
        public User User { get; set; }
        public int MembershipId { get; set; }
        public Membership Membership { get; set; }
    }
}