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

namespace SmartCqrs.Application.DomainEventHandlers.BlogCommented
{
    public class UpdateUserTotalCommentBlogCountEventHandler : INotificationHandler<BlogCommentedDomainEvent>
    {
        private readonly IUserAssetRepository _userAssetRepository;
        private readonly IRepository<BlogComment> _blogCommentRepository;

        public UpdateUserTotalCommentBlogCountEventHandler(IUserAssetRepository userAssetRepository, 
            IRepository<BlogComment> blogCommentRepository)
        {
            _userAssetRepository = userAssetRepository;
            _blogCommentRepository = blogCommentRepository;
        }

        public async Task Handle(BlogCommentedDomainEvent notification, CancellationToken cancellationToken)
        {
            // 检查用户是否已经评论过该博客，如果已评论，则评论博客的总数量不加1
            int count = await _blogCommentRepository.CountAsync(b => b.BlogId == notification.BlogComment.BlogId && b.UserId == notification.BlogComment.UserId);
            if(count > 0)
            {
                return;
            }

            // 当用户评论了博客文章后，其评论博客的总数量加1
            UserAsset userAsset = await _userAssetRepository.GetOrCreateAsync(notification.BlogComment.UserId);
            userAsset.IncreaseCommentBlogCount();
            await _userAssetRepository.InsertOrUpdateAsync(userAsset);
        }
    }
}
