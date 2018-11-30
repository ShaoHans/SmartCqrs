using System;
using MediatR;

namespace SmartCqrs.Domain.Events
{
    public class CarCollectedDomainEvent : INotification
    {
        public int CarId { get; }

        public Guid UserId { get; }

        public CarCollectedDomainEvent( int carId, Guid userId)
        {
            CarId = carId;
            UserId = userId;
        }
    }
}
