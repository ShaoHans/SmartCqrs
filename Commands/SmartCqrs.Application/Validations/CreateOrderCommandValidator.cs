using SmartCqrs.Application.Commands;
using FluentValidation;

namespace SmartCqrs.Application.Validations
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(r => r.CarId).GreaterThan(0).WithMessage("车辆Id不能为空");
            RuleFor(r => r.Qty).GreaterThan(0).WithMessage("购买数量必须大于0");
            RuleFor(r => r.LoginUserId).NotEmpty().WithMessage("用户Id不能为空");
        }
    }
}
