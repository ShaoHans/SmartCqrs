using SmartCqrs.Enumeration;
using Newtonsoft.Json;
using System;

namespace SmartCqrs.Query.ViewModels
{
    /// <summary>
    /// 车源列表展示Model，不需要显示所有字段
    /// </summary>
    public class CarListVm
    {
        /// <summary>
        /// 车源Id
        /// </summary>
        public int CarId { get; set; }
        
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
        /// 车规格名称
        /// </summary>
        public string SizeName { get; set; }

        /// <summary>
        /// 售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 发布车源用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 状态（1：售卖中，2：已下架，9：已删除）
        /// </summary>
        public CarStatus Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName
        {
            get
            {
                return Status.ToDescription();
            }
        }

        /// <summary>
        /// 车辆主图
        /// </summary>
        public string DefaultImage
        {
            get
            {
                string[] images = Image.DeserializeObject<string[]>();
                if (images == null || images.Length < 1)
                {
                    return string.Empty;
                }
                return images[0];
            }
        }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Colors
        {
            get
            {
                string[] colors = Color.DeserializeObject<string[]>();
                return string.Join("/", colors);
            }
        }

        #region JsonIgnore

        /// <summary>
        /// 车辆图片
        /// </summary>
        [JsonIgnore]
        public string Image { get; set; }

        /// <summary>
        /// 车辆颜色内饰
        /// </summary>
        [JsonIgnore]
        public string Color { get; set; }

        #endregion
    }
}
