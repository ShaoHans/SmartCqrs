using Newtonsoft.Json;
using System;

namespace SmartCqrs.Query.ViewModels
{
    public class CarDetailVm : CarListVm
    {

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockQty { get; set; }

        /// <summary>
        /// 浏览数量
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 产生的订单数量
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// 车辆图片集合
        /// </summary>
        public string[] Images
        {
            get
            {
                string[] images = Image.DeserializeObject<string[]>();
                if (images == null || images.Length < 1)
                {
                    return new string[] { };
                }
                return images;
            }
        }

    }
}
