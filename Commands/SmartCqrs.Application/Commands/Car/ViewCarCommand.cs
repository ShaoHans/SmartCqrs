using SmartCqrs.Infrastructure.Attributes;

namespace SmartCqrs.Application.Commands
{
    /// <summary>
    /// 浏览车辆信息
    /// </summary>
    public class ViewCarCommand : BaseCommand
    {
        /// <summary>
        /// 车辆Id/寻车Id
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }
    }
}
