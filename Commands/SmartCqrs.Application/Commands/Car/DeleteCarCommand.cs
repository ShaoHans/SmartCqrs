namespace SmartCqrs.Application.Commands
{
    public class DeleteCarCommand : BaseCommand
    {
        /// <summary>
        /// 车辆Id
        /// </summary>
        public int CarSourceId { get; set; }
    }
}
