using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Members.Commands.DeleteMember
{
    public class DeleteMemberCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}