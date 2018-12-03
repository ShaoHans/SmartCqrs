using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using SmartCqrs.Application.Commands;

namespace SmartCqrs.Application.Validations
{
    public class EditBlogCommandValidator : AbstractValidator<EditBlogCommand>
    {
        public EditBlogCommandValidator()
        {
            RuleFor(r => r.BlogId).GreaterThan(0).WithMessage("博客Id必须大于0");
            RuleFor(r => r.LoginUserId).NotEmpty().WithMessage("用户id不能为空");
            RuleFor(r => r.Title).NotEmpty().WithMessage("博客标题不能为空");
            RuleFor(r => r.Title).Must(t =>
            {
                return !string.IsNullOrWhiteSpace(t) && t.Length <= 100;
            }).WithMessage("博客标题长度不能超过100个字符");
            RuleFor(r => r.Content).NotEmpty().WithMessage("博客内容不能为空");
        }
    }
}
