using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.UpdateMembershipType
{
    public class UpdateMembershipTypeCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}