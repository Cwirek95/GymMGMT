using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Memberships.Commands.ChangeMembershipPrice
{
    public class ChangeMembershipPriceCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public double Price { get; set; }
    }
}