namespace GymMGMT.Application.CQRS.Trainers.Queries.GetTrainerDetail
{
    public class GetTrainerDetailQuery : IRequest<TrainerDetailViewModel>
    {
        public int Id { get; set; }
    }
}