using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SmartCqrs.Domain.Models;

namespace SmartCqrs.Domain.Events
{
    public class BlogCommentedDomainEvent: INotification
    {
        public BlogComment BlogComment { get; }

        public BlogCommentedDomainEvent(BlogComment blogComment)
        {
            BlogComment = blogComment;
        }
    }
}
