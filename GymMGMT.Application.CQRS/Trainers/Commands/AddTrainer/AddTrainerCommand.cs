using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainers.Commands.AddTrainer
{
    public class AddTrainerCommand : IRequest<ICommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get; set; }
    }
}