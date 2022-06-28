using GymMGMT.Domain.Enums;

namespace GymMGMT.Application.CQRS.Members.Queries.GetMemberDetail
{
    public class TrainingDto
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public TrainingType TrainingType { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
    }
}