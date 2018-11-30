using System;
using System.Threading;
using System.Threading.Tasks;
using SmartCqrs.Application.Validations;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Enumeration;
using SmartCqrs.Infrastructure.Results;
using SmartCqrs.Infrastructure.Sequence;

namespace SmartCqrs.Application.Commands
{
    public class CreateOrderCommandHandler : BaseCommandHandler<CreateOrderCommand, int>
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Car> _carRepository;
        public CreateOrderCommandHandler(IRepository<Order> orderRepository, 
            IRepository<Car> carRepository,
            IUnitOfWork uow) : base(uow)
        {
            _orderRepository = orderRepository;
            _carRepository = carRepository;
        }

        public override async Task<CommandResult<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var validateResult = await ValidateCommand(request, new CreateOrderCommandValidator());
            if(!validateResult.Success)
            {
                return new CommandResult<int>(validateResult.Code, validateResult.Message);
            }

            var car = await _carRepository.GetAsync(request.CarId);
            if (car == null)
            {
                return new CommandResult<int>(ResultCode.FAIL, "无效的车辆Id");
            }

            if (car.UserId.Equals(request.LoginUserId))
            {
                return new CommandResult<int>(ResultCode.FAIL, "不能对自己的车辆产生订单");
            }

            if (car.Status == CarStatus.Unshelved || car.Status == CarStatus.Deleted)
            {
                return new CommandResult<int>(ResultCode.FAIL, $"该车辆{car.Status.ToDescription()}");
            }

            string orderNo = IdWorkerHelper.GenerateSequenceNo();
            Order order = new Order(orderNo, request.CarId, request.Qty, car.SalesPrice, request.LoginUserId);
            await _orderRepository.InsertAsync(order);
            await Uow.SaveChangesAsync();
            return new CommandResult<int>(data: order.Id);
        }
    }
}
