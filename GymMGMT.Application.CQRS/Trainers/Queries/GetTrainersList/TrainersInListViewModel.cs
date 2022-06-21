namespace GymMGMT.Application.CQRS.Trainers.Queries.GetTrainersList
{
    public class TrainersInListViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Status { get; set; }
    }
}