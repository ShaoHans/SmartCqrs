using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCqrs.Application.Commands
{
    /// <summary>
    /// 评论博客
    /// </summary>
    public class CommentBlogCommand : BaseCommand
    {
        /// <summary>
        /// 博客Id
        /// </summary>
        public int BlogId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
    }
}
