using System;
using MediatR;

namespace SmartCqrs.Domain.Events
{
    public class CarCollectionCancelledDomainEvent : INotification
    {
        public int CarId { get; }

        public Guid UserId { get; }

        public CarCollectionCancelledDomainEvent(int carId, Guid userId)
        {
            CarId = carId;
            UserId = userId;
        }
    }
}
