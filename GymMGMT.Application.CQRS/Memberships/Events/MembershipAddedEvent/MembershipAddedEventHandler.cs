using GymMGMT.Application.CQRS.Members.Commands.SetMembershipToMember;

namespace GymMGMT.Application.CQRS.Memberships.Events.MembershipAddedEvent
{
    public class MembershipAddedEventHandler : INotificationHandler<MembershipAddedEvent>
    {
        private readonly IMediator _mediator;

        public MembershipAddedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(MembershipAddedEvent notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new SetMembershipToMemberCommand()
            {
                MemberId = notification.MemberId,
                MembershipId = notification.MembershipId
            });
        }
    }
}