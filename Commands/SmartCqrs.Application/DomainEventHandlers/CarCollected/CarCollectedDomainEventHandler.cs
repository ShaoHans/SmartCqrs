using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;
using SmartCqrs.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCqrs.Application.DomainEventHandlers.CarSourceCollected
{
    public class CarCollectedDomainEventHandler : INotificationHandler<CarCollectedDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Car> _carRepository;
        private readonly IUserAssetRepository _userAssetRepository;

        public CarCollectedDomainEventHandler(
            IRepository<Car> carRepository,
            IUserAssetRepository userAssetRepository,
            IUnitOfWork unitOfWork)
        {
            _carRepository = carRepository;
            _userAssetRepository = userAssetRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CarCollectedDomainEvent notification, CancellationToken cancellationToken)
        {
            Car car = await _carRepository.GetAsync(notification.CarId);
            if (car == null)
            {
                return;
            }
            car.IncreseCollectCount();
            await _carRepository.UpdateAsync(car);

            UserAsset userAsset = await _userAssetRepository.GetOrCreateAsync(notification.UserId);
            userAsset.IncreaseCollectCount();
            await _userAssetRepository.InsertOrUpdateAsync(userAsset);
        }
    }
}
