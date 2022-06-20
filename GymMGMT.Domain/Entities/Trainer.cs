using GymMGMT.Domain.Common;

namespace GymMGMT.Domain.Entities
{
    public class Trainer : AuditableEntity, IEntity<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Status { get; set; }

        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public IEnumerable<Training>? Trainings { get; set; }
    }
}