using SmartCqrs.Infrastructure.Attributes;

namespace SmartCqrs.Application.Commands
{
    /// <summary>
    /// 收藏车辆
    /// </summary>
    public class CollectCarCommand : BaseCommand
    {
        /// <summary>
        /// 车辆Id/寻车Id
        /// </summary>
        public int CarId { get; set; }
    }
}
