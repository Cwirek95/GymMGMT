namespace GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingDetail
{
    public class MemberDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
    }
}