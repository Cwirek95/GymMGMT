using GymMGMT.Application.CQRS.Memberships.Commands.AddMembership;

namespace GymMGMT.Application.CQRS.Members.Events.MemberAdded
{
    public class MemberAddedEventHandler : INotificationHandler<MemberAddedEvent>
    {
        private readonly IMediator _mediator;

        public MemberAddedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(MemberAddedEvent notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new AddMembershipCommand()
            {
                MemberId = notification.MemberId,
                MembershipTypeId = notification.MembershipTypeId,
                Price = notification.Price
            });
        }
    }
}