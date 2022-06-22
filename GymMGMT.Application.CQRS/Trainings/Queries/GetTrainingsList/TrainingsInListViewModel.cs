namespace GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingsList
{
    public class TrainingsInListViewModel
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string TrainerFirstName { get; set; }
        public string TrainerLastName { get; set; }
        public bool Status { get; set; }
    }
}