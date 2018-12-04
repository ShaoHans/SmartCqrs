using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;

namespace SmartCqrs.Application.DomainEventHandlers.BlogCommented
{
    public class UpdateBlogBeCommentedCountEventHandler : INotificationHandler<BlogCommentedDomainEvent>
    {
        private readonly IRepository<Blog> _blogRepository;

        public UpdateBlogBeCommentedCountEventHandler(IRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task Handle(BlogCommentedDomainEvent notification, CancellationToken cancellationToken)
        {
            // 博客被评论数量加1
            var blog = await _blogRepository.GetAsync(notification.BlogComment.BlogId);
            if (blog == null)
            {
                return;
            }

            blog.IncreaseCommentCount();
            await _blogRepository.UpdateAsync(blog);
        }
    }
}
