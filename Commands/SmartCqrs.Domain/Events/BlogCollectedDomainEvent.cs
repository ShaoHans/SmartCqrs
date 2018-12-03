using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Domain.Events
{
    public class BlogCollectedDomainEvent : INotification
    {
        public BlogCollect BlogCollect { get; }

        public BlogCollectedDomainEvent(BlogCollect blogCollect)
        {
            BlogCollect = blogCollect;
        }
    }
}
