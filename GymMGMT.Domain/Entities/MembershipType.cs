using GymMGMT.Domain.Common;

namespace GymMGMT.Domain.Entities
{
    public class MembershipType : AuditableEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationInDays { get; set; }
        public bool Status { get; set; }
    }
}