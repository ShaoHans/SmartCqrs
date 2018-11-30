using SmartCqrs.Infrastructure.Attributes;

namespace SmartCqrs.Application.Commands
{
    /// <summary>
    /// 取消收藏车辆
    /// </summary>
    public class CancelCarCollectionCommand : BaseCommand
    {
        /// <summary>
        /// 车辆Id/寻车Id
        /// </summary>
        public int CarId { get; set; }
    }
}
