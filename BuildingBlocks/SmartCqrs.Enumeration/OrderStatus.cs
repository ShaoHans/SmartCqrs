using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCqrs.Enumeration
{
    public enum OrderStatus
    {
        /// <summary>
        /// 待支付
        /// </summary>
        UnPay = 0,

        /// <summary>
        /// 已支付
        /// </summary>
        Paid = 1,
        
        /// <summary>
        /// 已发货
        /// </summary>
        Delivered = 2,

        /// <summary>
        /// 已签收
        /// </summary>
        Signed = 3,

        /// <summary>
        /// 已完成
        /// </summary>
        Finished = 4,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 5,

        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = 9
    }
}
