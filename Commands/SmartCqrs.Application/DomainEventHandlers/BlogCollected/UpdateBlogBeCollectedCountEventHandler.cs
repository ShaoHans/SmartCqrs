using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;

namespace SmartCqrs.Application.DomainEventHandlers.BlogCollected
{
    public class UpdateBlogBeCollectedCountEventHandler : INotificationHandler<BlogCollectedDomainEvent>
    {
        private readonly IRepository<Blog> _blogRepository;

        public UpdateBlogBeCollectedCountEventHandler(IRepository<Blog> blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task Handle(BlogCollectedDomainEvent notification, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetAsync(notification.BlogCollect.BlogId);
            if(blog == null)
            {
                return;
            }

            blog.IncreaseCollectCount();
            await _blogRepository.UpdateAsync(blog);
        }
    }
}
