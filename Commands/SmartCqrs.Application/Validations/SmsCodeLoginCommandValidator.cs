using SmartCqrs.Application.Commands;
using FluentValidation;
using System.Text.RegularExpressions;

namespace SmartCqrs.Application.Validations
{
    public class SmsCodeLoginCommandValidator : AbstractValidator<SmsCodeLoginCommand>
    {
        public SmsCodeLoginCommandValidator()
        {
            RuleFor(r => r.Mobile).Must(mobile =>
              {
                  return Regex.IsMatch(mobile, @"^1[345678]\d{9}$");
              }).WithMessage("请输入正确的手机号码");

            RuleFor(r => r.SmsCode).NotEmpty().WithMessage("验证码不能为空");
        }
    }
}
