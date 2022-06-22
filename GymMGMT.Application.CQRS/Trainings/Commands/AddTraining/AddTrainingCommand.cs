using GymMGMT.Application.Responses;
using GymMGMT.Domain.Enums;

namespace GymMGMT.Application.CQRS.Trainings.Commands.AddTraining
{
    public class AddTrainingCommand : IRequest<ICommandResponse>
    {
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public double Price { get; set; }
        public TrainingType TrainingType { get; set; }
        public int TrainerId { get; set; }
    }
}