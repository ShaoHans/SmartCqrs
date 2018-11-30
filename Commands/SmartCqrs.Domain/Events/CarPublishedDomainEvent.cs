using MediatR;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Domain.Events
{
    public class CarPublishedDomainEvent : INotification
    {
        public Car Car { get; }

        public CarPublishedDomainEvent(Car car)
        {
            Car = car;
        }
    }
}
