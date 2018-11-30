using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;
using SmartCqrs.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCqrs.Application.DomainEventHandlers.OrderCreated
{
    public class UpdateCarOrderCountWhenOrderCreatedDomainEventHandler
        : INotificationHandler<OrderCreatedDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Car> _carRepository;
        private readonly IUserAssetRepository _userAssetRepository;

        public UpdateCarOrderCountWhenOrderCreatedDomainEventHandler(IUnitOfWork unitOfWork,
            IRepository<Car> carRepository,
            IUserAssetRepository userAssetRepository)
        {
            _unitOfWork = unitOfWork;
            _carRepository = carRepository;
            _userAssetRepository = userAssetRepository;
        }

        public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            if (notification.Order == null || notification.Order.CarId <= 0)
            {
                return;
            }

            Car car = await _carRepository.GetAsync(notification.Order.CarId);
            if (car != null)
            {
                car.IncreaseOrderCount();
                await _carRepository.UpdateAsync(car);
            }

            UserAsset buyer = await _userAssetRepository.GetOrCreateAsync(notification.Order.UserId);
            buyer.IncreaseOrderCount();
            await _userAssetRepository.InsertOrUpdateAsync(buyer);
        }
    }
}
