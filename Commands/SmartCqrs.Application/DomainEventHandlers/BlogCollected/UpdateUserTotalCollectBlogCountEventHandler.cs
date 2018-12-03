using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;

namespace SmartCqrs.Application.DomainEventHandlers.BlogCollected
{
    public class UpdateUserTotalCollectBlogCountEventHandler : INotificationHandler<BlogCollectedDomainEvent>
    {
        private readonly IUserAssetRepository _userAssetRepository;

        public UpdateUserTotalCollectBlogCountEventHandler(IUserAssetRepository userAssetRepository)
        {
            _userAssetRepository = userAssetRepository;
        }

        public async Task Handle(BlogCollectedDomainEvent notification, CancellationToken cancellationToken)
        {
            // 当用户收藏了博客文章后，其收藏总数量加1
            UserAsset userAsset = await _userAssetRepository.GetOrCreateAsync(notification.BlogCollect.UserId);
            userAsset.IncreaseCollectBlogCount();
            await _userAssetRepository.InsertOrUpdateAsync(userAsset);
        }
    }
}
