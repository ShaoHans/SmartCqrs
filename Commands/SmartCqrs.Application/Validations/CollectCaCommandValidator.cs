using SmartCqrs.Application.Commands;
using FluentValidation;
using System;

namespace SmartCqrs.Application.Validations
{
    public class CollectCaCommandValidator : AbstractValidator<CollectCarCommand>
    {
        public CollectCaCommandValidator()
        {
            RuleFor(r => r.CarId).GreaterThan(0).WithMessage("无效的车辆Id/寻车Id");
            RuleFor(r => r.LoginUserId).NotEqual(Guid.Empty).WithMessage("收藏用户id不能为空");
        }
    }
}
