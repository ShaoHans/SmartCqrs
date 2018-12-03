using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartCqrs.Application.Validations;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Infrastructure.Results;

namespace SmartCqrs.Application.Commands
{
    public class EditBlogCommandHandler : BaseCommandHandler<EditBlogCommand>
    {
        private readonly IRepository<Blog> _blogRepository;

        public EditBlogCommandHandler(IUnitOfWork uow,
            IRepository<Blog> blogRepository) : base(uow)
        {
            _blogRepository = blogRepository;
        }

        public override async Task<CommandResult> Handle(EditBlogCommand request, CancellationToken cancellationToken)
        {
            var validateResult = await ValidateCommand(request, new EditBlogCommandValidator());
            if (!validateResult.Success)
            {
                return validateResult;
            }

            Blog blog = await _blogRepository.GetAsync(request.BlogId);
            if(blog == null)
            {
                return new CommandResult(ResultCode.NOT_FOUND, "博客文章不存在");
            }

            if (blog.UserId != request.LoginUserId)
            {
                return new CommandResult(ResultCode.FAIL, "只允许编辑自己的博客");
            }

            blog.Edit(request.Title, request.Content, request.CoverUrl);
            await _blogRepository.UpdateAsync(blog);
            await UnitOfWork.SaveChangesAsync();

            return new CommandResult();
        }
    }
}
