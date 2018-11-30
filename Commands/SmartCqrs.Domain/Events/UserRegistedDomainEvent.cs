using MediatR;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Domain.Events
{
    public class UserRegistedDomainEvent: INotification
    {
        public User User { get; }

        public UserRegistedDomainEvent(User user)
        {
            User = user;
        }
    }
}
