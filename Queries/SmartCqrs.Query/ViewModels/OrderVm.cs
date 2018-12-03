using SmartCqrs.Enumeration;
using Newtonsoft.Json;
using System;

namespace SmartCqrs.Query.ViewModels
{
    public class OrderVm
    {
        public int OrderId { get; set; }

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
        public int OrderQty { get; set; }

        /// <summary>
        /// 订单价格
        /// </summary>
        public decimal? OrderPrice { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        //public OrderStatus Status { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime OrderDate { get; set; }

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
        /// 车辆图片集合
        /// </summary>
        public string[] Images
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Image))
                {
                    return new string[] { };
                }
                string[] images = Image.DeserializeObject<string[]>();
                if (images == null || images.Length < 1)
                {
                    return new string[] { };
                }
                return images;
            }
        }

        /// <summary>
        /// 车辆图片
        /// </summary>
        [JsonIgnore]
        public string Image { get; set; }
    }
}
