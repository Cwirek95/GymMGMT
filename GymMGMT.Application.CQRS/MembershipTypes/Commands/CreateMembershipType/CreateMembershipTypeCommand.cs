using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.CreateMembershipType
{
    public class CreateMembershipTypeCommand : IRequest<ICommandResponse>
    {
        public string Name { get; set; }
        public double DefaultPrice { get; set; }
        public int DurationInDays { get; set; }
    }
}