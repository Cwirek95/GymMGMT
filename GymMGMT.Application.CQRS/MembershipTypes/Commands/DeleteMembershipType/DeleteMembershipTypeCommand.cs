using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.MembershipTypes.Commands.DeleteMembershipType
{
    public class DeleteMembershipTypeCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}