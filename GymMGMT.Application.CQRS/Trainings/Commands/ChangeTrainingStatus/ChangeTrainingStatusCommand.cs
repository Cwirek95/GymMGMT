using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingStatus
{
    public class ChangeTrainingStatusCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}