using SmartCqrs.Enumeration;

namespace SmartCqrs.Application.Commands
{
    /// <summary>
    /// 发布车辆/编辑车辆/重新发布
    /// </summary>
    public class PublishCarCommand : BaseCommand<int>
    {
        /// <summary>
        /// 发布模式
        /// <Remark>
        /// 1：新发布
        /// 2：编辑
        /// 3：重新发布
        /// </Remark>
        /// </summary>
        public PublishMode PublishMode { get; set; }

        /// <summary>
        /// 车辆Id
        /// <Remark>
        /// 当发布模式为1时，车辆Id可为0；其他情况不能为0；
        /// </Remark>
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
        /// 车辆颜色内饰
        /// </summary>
        public string[] Colors { get; set; }

        /// <summary>
        /// 车规格Id
        /// </summary>
        public int SizeId { get; set; }

        /// <summary>
        /// 车规格名称
        /// </summary>
        public string SizeName { get; set; }

        /// <summary>
        /// 售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 指导价
        /// </summary>
        public decimal GuidePrice { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 车辆图片集合
        /// </summary>
        public string[] Images { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockQty { get; set; }
    }
}
