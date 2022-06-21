using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainers.Commands.UpdateTrainer
{
    public class UpdateTrainerCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}