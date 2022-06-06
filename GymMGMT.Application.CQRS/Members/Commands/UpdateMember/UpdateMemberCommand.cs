using GymMGMT.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMGMT.Application.CQRS.Members.Commands.UpdateMember
{
    public class UpdateMemberCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }
}