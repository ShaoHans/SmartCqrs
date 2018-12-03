using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Domain.Events
{
    /// <summary>
    /// 发布博客文章领域事件
    /// </summary>
    public class BlogPublishedDomainEvent : INotification
    {
        public Blog Blog { get; }

        public BlogPublishedDomainEvent(Blog blog)
        {
            Blog = blog;
        }
    }
}
