using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingType
{
    public class ChangeTrainingTypeCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}