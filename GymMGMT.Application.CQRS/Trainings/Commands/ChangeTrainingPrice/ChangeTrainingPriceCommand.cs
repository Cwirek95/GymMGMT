using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Trainings.Commands.ChangeTrainingPrice
{
    public class ChangeTrainingPriceCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public double Price { get; set; }
    }
}