using GymMGMT.Domain.Enums;

namespace GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingDetail
{
    public class TrainingDetailViewModel
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public TrainingType TrainingType { get; set; }
        public double Price { get; set; }
        public string TrainerFirstName { get; set; }
        public string TrainerLastName { get; set; }
        public IEnumerable<MemberDto> Members { get; set; }
        public bool Status { get; set; }
    }
}