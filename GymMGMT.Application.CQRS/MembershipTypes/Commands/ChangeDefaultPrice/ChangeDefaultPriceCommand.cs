using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.ChangeDefaultPrice
{
    public class ChangeDefaultPriceCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public double DefaultPrice { get; set; }
    }
}