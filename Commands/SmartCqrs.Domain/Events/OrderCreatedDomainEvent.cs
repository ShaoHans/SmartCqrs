using MediatR;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Domain.Events
{
    public class OrderCreatedDomainEvent : INotification
    {
        public Order Order { get; }

        public OrderCreatedDomainEvent(Order order)
        {
            Order = order;
        }
    }
}
