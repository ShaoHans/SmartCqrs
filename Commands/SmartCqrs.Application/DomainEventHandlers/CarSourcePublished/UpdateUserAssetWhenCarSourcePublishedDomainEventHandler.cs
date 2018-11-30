using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SmartCqrs.Enumeration;

namespace SmartCqrs.Application.DomainEventHandlers.CarSourcePublished
{
    public class UpdateUserAssetWhenCarSourcePublishedDomainEventHandler
        : INotificationHandler<CarPublishedDomainEvent>
    {
        private readonly IUserAssetRepository _userAssetRepository;

        public UpdateUserAssetWhenCarSourcePublishedDomainEventHandler(IUserAssetRepository userAssetRepository)
        {
            _userAssetRepository = userAssetRepository;
        }

        public async Task Handle(CarPublishedDomainEvent notification, CancellationToken cancellationToken)
        {
            int stockQty = 0;
            if (notification.Car.Status == CarStatus.Deleted || notification.Car.Status == CarStatus.Unshelved)
            {
                stockQty = 0 - notification.Car.StockQty;
            }
            else if(notification.Car.Status == CarStatus.Selling)
            {
                stockQty = notification.Car.StockQty;
            }

            UserAsset userAsset = await _userAssetRepository.GetOrCreateAsync(notification.Car.UserId);
            userAsset.UpdateSellingCarCount(stockQty);
            await _userAssetRepository.InsertOrUpdateAsync(userAsset);
        }
    }
}
