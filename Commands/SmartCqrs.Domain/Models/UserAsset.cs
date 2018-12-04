using System;
using System.Collections.Generic;
using System.Text;
using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Enumeration;

namespace SmartCqrs.Domain.Models
{
    public class UserAsset : Entity
    {
        /// <summary>
        /// 用户uuid
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 总积分
        /// </summary>
        public int TotalPoint { get; set; }

        /// <summary>
        /// 发布的博客文章数量
        /// </summary>
        public int PublishBlogCount { get; set; }

        /// <summary>
        /// 收藏的博客文章数量
        /// </summary>
        public int CollectBlogCount { get; set; }

        /// <summary>
        /// 评论的博客文章数量
        /// </summary>
        public int CommentBlogCount { get; set; }

        #region 领域方法

        /// <summary>
        /// 用户发布的博客数量加1
        /// </summary>
        public void IncreasePublishBlogCount()
        {
            PublishBlogCount += 1;
        }

        /// <summary>
        /// 增加积分
        /// </summary>
        /// <param name="point"></param>
        public void AddPoint(int point)
        {
            TotalPoint += point;
        }

        /// <summary>
        /// 用户收藏的博客数量加1
        /// </summary>
        public void IncreaseCollectBlogCount()
        {
            CollectBlogCount += 1;
        }

        /// <summary>
        /// 用户评论的博客数量加1
        /// </summary>
        public void IncreaseCommentBlogCount()
        {
            CommentBlogCount += 1;
        }

        #endregion
    }
}
