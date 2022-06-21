using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainers.Commands.DeleteTrainer
{
    public class DeleteTrainerCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}