namespace SmartCqrs.Application.Commands
{
    public class DeleteOrderCommand : BaseCommand
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 关闭原因
        /// </summary>
        public string Reason { get; set; }
    }
}
