using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.SeedWork;
using System;

namespace SmartCqrs.Domain.Models
{
    /// <summary>
    /// 收藏车辆
    /// </summary>
    public class CollectCar : Entity
    {
        /// <summary>
        /// 当收藏类别为1时表示车辆详情Id，当收藏类别为2时表示寻车详情Id
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// 收藏用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime CollectedTime { get; set; }

        /// <summary>
        /// 收藏
        /// </summary>
        public void Collect()
        {
            AddDomainEvent(new CarCollectedDomainEvent(CarId, UserId));
        }

        /// <summary>
        /// 取消收藏
        /// </summary>
        public void CancelCollect()
        {
            AddDomainEvent(new CarCollectionCancelledDomainEvent(CarId, UserId));
        }
    }
}
