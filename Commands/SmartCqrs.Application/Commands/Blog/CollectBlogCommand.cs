using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCqrs.Application.Commands
{
    public class CollectBlogCommand : BaseCommand
    {
        /// <summary>
        /// 博客Id
        /// </summary>
        public int BlogId { get; set; }
    }
}
