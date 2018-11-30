using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCqrs.Enumeration
{
    public enum RegisterChannel
    {
        /// <summary>
        /// APP注册
        /// </summary>
        App = 0,

        /// <summary>
        /// 后台手动添加
        /// </summary>
        ManuallyAdd = 1,

        /// <summary>
        /// Web注册
        /// </summary>
        Web = 2
    }
}
