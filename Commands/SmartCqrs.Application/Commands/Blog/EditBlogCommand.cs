using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCqrs.Application.Commands
{
    public class EditBlogCommand: BaseCommand
    {
        /// <summary>
        /// 博客Id
        /// </summary>
        public int BlogId { get; set; }

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
    }
}
