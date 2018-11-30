using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;
using SmartCqrs.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCqrs.Application.DomainEventHandlers.CarCollectionCancelled
{
    public class CarCollectionCancelledDomainEventHandler :
        INotificationHandler<CarCollectionCancelledDomainEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Car> _carRepository;
        private readonly IUserAssetRepository _userAssetRepository;

        public CarCollectionCancelledDomainEventHandler(IUnitOfWork unitOfWork,
            IRepository<Car> carRepository,
            IUserAssetRepository userAssetRepository)
        {
            _carRepository = carRepository;
            _userAssetRepository = userAssetRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CarCollectionCancelledDomainEvent notification, CancellationToken cancellationToken)
        {
            Car carSource = await _carRepository.GetAsync(notification.CarId);
            if (carSource == null)
            {
                return;
            }
            carSource.DecreaseCollectCount();
            await _carRepository.UpdateAsync(carSource);

            UserAsset userAsset = await _userAssetRepository.GetOrCreateAsync(notification.UserId);
            userAsset.DecreaseCollectCount();
            await _userAssetRepository.InsertOrUpdateAsync(userAsset);
        }
    }
}
