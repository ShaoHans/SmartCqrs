using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.SeedWork;
using System;

namespace SmartCqrs.Domain.Models
{
    /// <summary>
    /// 用户资产信息
    /// </summary>
    public class UserAsset: Entity
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

        public DateTime UpdatedTime { get; set; }

        #region 领域方法

        public void Init(Guid userId)
        {
            UserId = userId;
            UpdatedTime = DateTime.Now;
        }

        /// <summary>
        /// 车收藏数量加1
        /// </summary>
        public void IncreaseCollectCount()
        {
            CollectCarCount += 1;
            UpdatedTime = DateTime.Now;
        }

        /// <summary>
        /// 车收藏数量加1
        /// </summary>
        public void DecreaseCollectCount()
        {
            CollectCarCount -= 1;
            UpdatedTime = DateTime.Now;
        }

        /// <summary>
        /// 更新在售车辆数量
        /// </summary>
        /// <param name="qty"></param>
        public void UpdateSellingCarCount(int qty)
        {
            SellingCarCount += qty;
            if (SellingCarCount < 0)
            {
                SellingCarCount = 0;
            }
            UpdatedTime = DateTime.Now;
        }

        /// <summary>
        /// 订单数量加1
        /// </summary>
        public void IncreaseOrderCount()
        {
            OrderCount += 1;
            UpdatedTime = DateTime.Now;
        }

        #endregion
    }
}
