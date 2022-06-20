using GymMGMT.Domain.Common;
using GymMGMT.Domain.Enums;

namespace GymMGMT.Domain.Entities
{
    public class Training : AuditableEntity, IEntity<int>
    {
        public int Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }


        public TrainingType TrainingType { get; set; }
        public IEnumerable<Member>? Members { get; set; }
        public int? TrainerId { get; set; }
        public Trainer? Trainer { get; set; }
    }
}