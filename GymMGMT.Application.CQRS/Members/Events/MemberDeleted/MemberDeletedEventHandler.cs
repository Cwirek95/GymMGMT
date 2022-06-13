using GymMGMT.Application.CQRS.Memberships.Commands.DeleteMembership;

namespace GymMGMT.Application.CQRS.Members.Events.MemberDeleted
{
    public class MemberDeletedEventHandler : INotificationHandler<MemberDeletedEvent>
    {
        private readonly IMediator _mediator;

        public MemberDeletedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(MemberDeletedEvent notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteMembershipCommand()
            {
                Id = notification.MembershipId
            });
        }
    }
}