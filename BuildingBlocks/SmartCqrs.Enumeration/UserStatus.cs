﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCqrs.Enumeration
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// 有效
        /// </summary>
        Actived = 0,

        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = 9
    }
}
