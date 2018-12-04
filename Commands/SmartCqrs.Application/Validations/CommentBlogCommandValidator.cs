using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using SmartCqrs.Application.Commands;

namespace SmartCqrs.Application.Validations
{
    public class CommentBlogCommandValidator : AbstractValidator<CommentBlogCommand>
    {
        public CommentBlogCommandValidator()
        {
            RuleFor(r => r.LoginUserId).NotEmpty().WithMessage("评论用户Id不能为空");
            RuleFor(r => r.BlogId).GreaterThan(0).WithMessage("博客Id必须大于0");
            RuleFor(r => r.Content).NotEmpty().WithMessage("评论内容不能为空");
            RuleFor(r => r.Content).MaximumLength(1000)
                .When(r => { return !string.IsNullOrWhiteSpace(r.Content); })
                .WithMessage("评论内容长度不能超过1000个字符");
            
        }
    }
}
