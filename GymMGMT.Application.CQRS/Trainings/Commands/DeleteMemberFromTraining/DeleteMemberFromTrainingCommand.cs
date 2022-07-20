using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.DeleteMemberFromTraining
{
    public class DeleteMemberFromTrainingCommand : IRequest<ICommandResponse>
    {
        public int TrainingId { get; set; }
        public int MemberId { get; set; }
    }
}