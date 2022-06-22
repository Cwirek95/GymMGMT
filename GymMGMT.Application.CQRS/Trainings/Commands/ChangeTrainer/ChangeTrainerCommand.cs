using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainer
{
    public class ChangeTrainerCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public int NewTrainerId { get; set; }
    }
}