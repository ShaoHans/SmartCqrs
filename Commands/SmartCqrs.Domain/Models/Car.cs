using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Enumeration;
using System;

namespace SmartCqrs.Domain.Models
{
    /// <summary>
    /// 发布的车辆
    /// </summary>
    public class Car : Entity
    {

        /// <summary>
        /// 车品牌名称
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 车系名称
        /// </summary>
        public string SeriesName { get; set; }

        /// <summary>
        /// 车款式名称
        /// </summary>
        public string StyleName { get; set; }

        /// <summary>
        /// 车型名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 车辆图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockQty { get; set; }

        /// <summary>
        /// 已售数量
        /// </summary>
        public int SalesQty { get; set; }

        /// <summary>
        /// 浏览数量
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 收藏数量
        /// </summary>
        public int CollectCount { get; set; }

        /// <summary>
        /// 产生的订单数量
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// 状态（1：售卖中，2：已下架，9：已删除）
        /// </summary>
        public CarStatus Status { get; set; }

        /// <summary>
        /// 发布车辆用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishedTime { get; set; }

        public Car()
        {

        }

        public Car(Guid userId)
        {
            UserId = userId;
            PublishedTime = DateTime.Now;
            Status = CarStatus.Selling;

            // 增加用户在售车辆数量
            AddDomainEvent(new CarPublishedDomainEvent(this));
        }

        /// <summary>
        /// 车辆被浏览次数加1
        /// </summary>
        public void IncreaseViewCount()
        {
            ViewCount = ViewCount + 1;
        }

        /// <summary>
        /// 车辆被收藏次数加1
        /// </summary>
        public void IncreseCollectCount()
        {
            CollectCount = CollectCount + 1;
        }

        /// <summary>
        /// 车辆被收藏次数减1
        /// </summary>
        public void DecreaseCollectCount()
        {
            CollectCount = CollectCount - 1;
        }

        /// <summary>
        /// 删除车辆
        /// </summary>
        public void Delete()
        {
            if (Status != CarStatus.Deleted)
            {
                Status = CarStatus.Deleted;
                AddDomainEvent(new CarPublishedDomainEvent(this));
            }
        }
        

        /// <summary>
        /// 复制车辆
        /// </summary>
        /// <returns></returns>
        public Car Clone()
        {
            Car carSource = new Car();

            carSource.BrandName = BrandName;
            carSource.SeriesName = SeriesName;
            carSource.StyleName = StyleName;
            carSource.ModelName = ModelName;
            carSource.SalesPrice = SalesPrice;
            carSource.Tag = Tag;
            carSource.Description = Description;
            carSource.Image = Image;
            carSource.StockQty = StockQty;
            carSource.Status = CarStatus.Selling;
            carSource.UserId = UserId;
            carSource.PublishedTime = DateTime.Now;

            return carSource;
        }

        /// <summary>
        /// 重新发布
        /// </summary>
        public void RePublish()
        {
            if (Status != CarStatus.Selling)
            {
                Status = CarStatus.Selling;
                // 增加用户在售车辆数量
                AddDomainEvent(new CarPublishedDomainEvent(this));
            }
        }

        /// <summary>
        /// 生成意向订单
        /// </summary>
        public void IncreaseOrderCount()
        {
            OrderCount = OrderCount + 1;
        }
    }
}
