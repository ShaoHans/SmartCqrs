using SmartCqrs.Application.Commands;
using FluentValidation;

namespace SmartCqrs.Application.Validations
{
    public class CloseOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public CloseOrderCommandValidator()
        {
            RuleFor(r => r.OrderId).GreaterThan(0).WithMessage("订单Id不能为空");
            RuleFor(r => r.Reason).NotEmpty().WithMessage("关闭原因不能为空");
        }
    }
}
