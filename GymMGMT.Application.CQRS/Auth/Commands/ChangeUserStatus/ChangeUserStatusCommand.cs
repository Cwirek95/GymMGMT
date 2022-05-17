using GymMGMT.Application.Responses;
using MediatR;

namespace GymMGMT.Application.CQRS.Auth.Commands.ChangeUserStatus
{
    public class ChangeUserStatusCommand : IRequest<ICommandResponse>
    {
        public Guid Id { get; set; }
    }
}