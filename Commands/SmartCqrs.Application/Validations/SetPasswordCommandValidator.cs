using SmartCqrs.Application.Commands;
using FluentValidation;

namespace SmartCqrs.Application.Validations
{
    public class SetPasswordCommandValidator : AbstractValidator<SetPasswordCommand>
    {
        public SetPasswordCommandValidator()
        {
            RuleFor(r => r.PhoneNo).NotEmpty().WithMessage("手机号码不能为空");
            RuleFor(r => r.SmsCode).NotEmpty().WithMessage("短信验证码不能为空");
            RuleFor(r => r.Password).NotEmpty().WithMessage("密码不能为空");
        }
    }
}
