using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCqrs.Application.DomainEventHandlers.CarViewed
{
    public class CarViewedDomainEventHandler : INotificationHandler<CarViewedDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Car> _carRepository;

        public CarViewedDomainEventHandler(IUnitOfWork unitOfWork, 
            IRepository<Car> carRepository)
        {
            _unitOfWork = unitOfWork;
            _carRepository = carRepository;
        }

        public async Task Handle(CarViewedDomainEvent notification, CancellationToken cancellationToken)
        {
            Car car = await _carRepository.GetAsync(notification.CarId);
            if (car != null)
            {
                car.IncreaseViewCount();
                await _carRepository.UpdateAsync(car);
            }
        }
    }
}
