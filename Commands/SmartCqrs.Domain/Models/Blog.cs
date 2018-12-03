using System;
using System.Collections.Generic;
using System.Text;
using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.SeedWork;

namespace SmartCqrs.Domain.Models
{
    /// <summary>
    /// 博客文章
    /// </summary>
    public class Blog : Entity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverUrl { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 收藏数量
        /// </summary>
        public int CollectCount { get; set; }

        /// <summary>
        /// 作者id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 发表时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedTime { get; set; }

        #region 领域方法

        /// <summary>
        /// 发布博客
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="coverUrl"></param>
        /// <param name="userId"></param>
        public void Publish(string title, string content, string coverUrl, Guid userId)
        {
            Title = title;
            Content = content;
            CoverUrl = coverUrl;
            UserId = userId;
            CreatedTime = DateTime.Now;

            // 博客发表之后触发相关领域事件（如：用户发布的博客总数量加1，增加用户积分等）
            AddDomainEvent(new BlogPublishedDomainEvent(this));
        }

        /// <summary>
        /// 编辑博客
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="coverUrl"></param>
        /// <param name="userId"></param>
        public void Edit(string title, string content, string coverUrl)
        {
            Title = title;
            Content = content;
            CoverUrl = coverUrl;
            UpdatedTime = DateTime.Now;
        }

        /// <summary>
        /// 博客被收藏数量加1
        /// </summary>
        public void IncreaseCollectCount()
        {
            CollectCount += 1;
        }

        #endregion
    }
}
