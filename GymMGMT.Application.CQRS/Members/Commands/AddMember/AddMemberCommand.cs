using GymMGMT.Application.Responses;

namespace GymMGMT.Application.CQRS.Members.Commands.AddMember
{
    public class AddMemberCommand : IRequest<CommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public Guid UserId { get; set; }
        public int MembershipTypeId { get; set; }
        public double? Price { get; set; }
    }
} 