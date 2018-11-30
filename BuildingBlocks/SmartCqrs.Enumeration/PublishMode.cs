using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCqrs.Enumeration
{
    /// <summary>
    /// 发布模式
    /// </summary>
    public enum PublishMode
    {
        /// <summary>
        /// 新发布
        /// </summary>
        New = 1,

        /// <summary>
        /// 编辑
        /// </summary>
        Edit = 2,

        /// <summary>
        /// 重新发布
        /// </summary>
        RePublish = 3
    }
}
