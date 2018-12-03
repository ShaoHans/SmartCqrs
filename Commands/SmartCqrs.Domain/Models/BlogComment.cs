using System;
using System.Collections.Generic;
using System.Text;
using SmartCqrs.Domain.SeedWork;

namespace SmartCqrs.Domain.Models
{
    /// <summary>
    /// 博客评论
    /// </summary>
    public class BlogComment : Entity
    {
        /// <summary>
        /// 博客Id
        /// </summary>
        public int BlogId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// 回复数量
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 评论人id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

    }
}
