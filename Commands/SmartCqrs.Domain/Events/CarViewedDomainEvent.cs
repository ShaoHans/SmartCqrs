using MediatR;

namespace SmartCqrs.Domain.Events
{
    public class CarViewedDomainEvent : INotification
    {
        /// <summary>
        /// 车Id
        /// </summary>
        public int CarId { get; }

        public CarViewedDomainEvent(int carId)
        {
            CarId = carId;
        }
    }
}
