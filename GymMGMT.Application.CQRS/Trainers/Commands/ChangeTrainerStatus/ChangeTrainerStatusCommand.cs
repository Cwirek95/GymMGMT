using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainers.Commands.ChangeTrainerStatus
{
    public class ChangeTrainerStatusCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}