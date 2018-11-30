namespace SmartCqrs.Application.Commands
{
    public class CreateOrderCommand : BaseCommand<int>
    {
        /// <summary>
        /// 车辆Id
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int Qty { get; set; }
    }
}
