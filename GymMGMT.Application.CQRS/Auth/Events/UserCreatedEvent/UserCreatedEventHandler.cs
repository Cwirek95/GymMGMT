using GymMGMT.Application.CQRS.Notifications.Commands;

namespace GymMGMT.Application.CQRS.Auth.Events.UserCreatedEvent
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IMediator _mediator;

        public UserCreatedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new SendEmailCommand()
            {
                Receiver = notification.User.Email,
                Subject = "GymMGMT - Account created!",
                Content = "Your account was created!"
            });
        }
    }
}