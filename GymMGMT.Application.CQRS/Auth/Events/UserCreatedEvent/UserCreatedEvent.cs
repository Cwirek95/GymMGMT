using GymMGMT.Domain.Entities;

namespace GymMGMT.Application.CQRS.Auth.Events.UserCreatedEvent
{
    public class UserCreatedEvent : INotification
    {
        public UserCreatedEvent(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }
}
