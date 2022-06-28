using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.AddMemberToTraining
{
    public class AddMemberToTrainingCommand : IRequest<ICommandResponse>
    {
        public int TrainingId { get; set; }
        public int MemberId { get; set; }
    }
}