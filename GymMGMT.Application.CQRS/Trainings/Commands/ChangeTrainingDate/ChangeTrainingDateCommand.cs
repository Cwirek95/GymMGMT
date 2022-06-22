using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingDate
{
    public class ChangeTrainingDateCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}