using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SmartCqrs.Enumeration
{
    public enum CarStatus
    {
        /// <summary>
        /// 售卖中
        /// </summary>
        [Description("售卖中")]
        Selling = 1,

        /// <summary>
        /// 已下架
        /// </summary>
        [Description("已下架")]
        Unshelved = 2,

        /// <summary>
        /// 已删除
        /// </summary>
        [Description("已删除")]
        Deleted = 9
    }
}
