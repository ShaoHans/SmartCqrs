using System;
using System.Collections.Generic;
using System.Text;
using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.SeedWork;

namespace SmartCqrs.Domain.Models
{
    /// <summary>
    /// 博客收藏记录
    /// </summary>
    public class BlogCollect : Entity
    {
        /// <summary>
        /// 博客Id
        /// </summary>
        public int BlogId { get; set; }

        /// <summary>
        /// 收藏用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        #region 领域方法

        public BlogCollect() { }

        public BlogCollect(int blogId, Guid userId)
        {
            BlogId = blogId;
            UserId = userId;
            CreatedTime = DateTime.Now;

            AddDomainEvent(new BlogCollectedDomainEvent(this));
        }

        #endregion
    }
}
