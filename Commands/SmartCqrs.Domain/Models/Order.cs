using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Enumeration;
using System;

namespace SmartCqrs.Domain.Models
{
    public class Order: Entity
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 车详情Id
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 订单价格
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 订单状态（0：待联系，1：已联系，8：已关闭，9：已删除）
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// 买方用户Id
        /// </summary>
        public Guid UserId { get; set; }
        
        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? UpdatedTime { get; set; }

        public Order() { }

        public Order(string orderNo, int carId, int buyQty, decimal? orderPirce, Guid userId)
        {
            OrderNo = orderNo;
            CarId = carId;
            Qty = buyQty;
            Price = orderPirce;
            UserId = userId;
            Status = OrderStatus.UnPay;
            OrderDate = DateTime.Now;

            AddDomainEvent(new OrderCreatedDomainEvent(this));
        }

        public void Delete()
        {
            Status = OrderStatus.Deleted;
        }
    }
}
