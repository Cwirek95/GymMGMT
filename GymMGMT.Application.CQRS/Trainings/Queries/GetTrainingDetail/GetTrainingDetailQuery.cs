namespace GymMGMT.Application.CQRS.Trainings.Queries.GetTrainingDetail
{
    public class GetTrainingDetailQuery : IRequest<TrainingDetailViewModel>
    {
        public int Id { get; set; }
    }
}