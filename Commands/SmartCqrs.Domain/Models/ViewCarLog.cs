using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.SeedWork;
using System;

namespace SmartCqrs.Domain.Models
{
    public class ViewCarLog : Entity
    {
        /// <summary>
        /// 车辆Id/寻车Id
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 浏览用户Id（若用户没有登录，则为空）
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// 浏览时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        public void View()
        {
            AddDomainEvent(new CarViewedDomainEvent(CarId));
        }
    }
}
