using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;
using SmartCqrs.Domain.SeedWork;

namespace SmartCqrs.Application.DomainEventHandlers.BlogPublished
{
    /// <summary>
    /// 发布博客文章领域事件处理器
    /// </summary>
    public class UpdateUserTotalPublishBlogCountEventHandler : INotificationHandler<BlogPublishedDomainEvent>
    {
        private readonly IUserAssetRepository _userAssetRepository;

        public UpdateUserTotalPublishBlogCountEventHandler(IUserAssetRepository userAssetRepository)
        {
            _userAssetRepository = userAssetRepository;
        }

        public async Task Handle(BlogPublishedDomainEvent notification, CancellationToken cancellationToken)
        {
            // 当用户发布了博客文章后，博客总数量加1
            UserAsset userAsset = await _userAssetRepository.GetOrCreateAsync(notification.Blog.UserId);
            userAsset.IncreasePublishBlogCount();
            await _userAssetRepository.InsertOrUpdateAsync(userAsset);
        }
    }
}
