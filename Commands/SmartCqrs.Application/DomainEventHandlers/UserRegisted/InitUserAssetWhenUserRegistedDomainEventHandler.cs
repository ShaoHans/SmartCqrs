using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCqrs.Application.DomainEventHandlers.UserRegisted
{
    public class InitUserAssetWhenUserRegistedDomainEventHandler : INotificationHandler<UserRegistedDomainEvent>
    {
        private readonly IUserAssetRepository _userAssetRepository;

        public InitUserAssetWhenUserRegistedDomainEventHandler(IUserAssetRepository userAssetRepository)
        {
            _userAssetRepository = userAssetRepository;
        }

        public async Task Handle(UserRegistedDomainEvent notification, CancellationToken cancellationToken)
        {
            UserAsset userAsset = new UserAsset();
            userAsset.Init(notification.User.UserId);
            await _userAssetRepository.InsertAsync(userAsset);
        }
    }
}
