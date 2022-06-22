using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.DeleteTraining
{
    public class DeleteTrainingCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}