using System;
using System.Threading;
using System.Threading.Tasks;
using SmartCqrs.Application.Validations;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Enumeration;
using SmartCqrs.Infrastructure.Results;

namespace SmartCqrs.Application.Commands
{
    public class DeleteOrderCommandHandler : BaseCommandHandler<DeleteOrderCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public DeleteOrderCommandHandler(IRepository<Order> orderRepository, IUnitOfWork uow) : base(uow)
        {
            _orderRepository = orderRepository;
        }

        public override async Task<CommandResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var validateResult = await ValidateCommand(request, new CloseOrderCommandValidator());
            if (!validateResult.Success)
            {
                return validateResult;
            }

            Order order = await _orderRepository.GetAsync(request.OrderId);
            if(order == null)
            {
                return new CommandResult(ResultCode.FAIL, "不存在该订单");
            }

            if (order.Status == OrderStatus.Deleted)
            {
                return new CommandResult(ResultCode.FAIL, $"该订单{order.Status.ToDescription()}");
            }

            order.Delete();
            await _orderRepository.UpdateAsync(order);
            await Uow.SaveChangesAsync();

            return new CommandResult();
        }
    }
}
