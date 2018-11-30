using SmartCqrs.Application.Commands;
using FluentValidation;
using System.Text.RegularExpressions;

namespace SmartCqrs.Application.Validations
{
    public class PasswordLoginCommandValidator : AbstractValidator<PasswordLoginCommand>
    {
        public PasswordLoginCommandValidator()
        {
            RuleFor(r => r.Mobile).Must(mobile =>
              {
                  return Regex.IsMatch(mobile, @"^1[345678]\d{9}$");
              }).WithMessage("请输入正确的手机号码");

            RuleFor(r => r.Password).NotEmpty().WithMessage("密码不能为空");
        }
    }
}
