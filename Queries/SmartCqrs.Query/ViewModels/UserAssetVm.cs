using Newtonsoft.Json;
using System;

namespace SmartCqrs.Query.ViewModels
{
    public class UserAssetVm
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 在售车辆数量
        /// </summary>
        public int SellingCarCount { get; set; }

        /// <summary>
        /// 收藏车数量
        /// </summary>
        public int CollectCarCount { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderCount { get; set; }

    }
}
