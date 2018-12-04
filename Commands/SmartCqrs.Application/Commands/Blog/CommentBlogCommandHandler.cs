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
    public class CommentBlogCommandHandler : BaseCommandHandler<CommentBlogCommand>
    {
        private readonly IRepository<BlogComment> _blogCommentRepository;
        private readonly IRepository<Blog> _blogRepository;

        public CommentBlogCommandHandler(IUnitOfWork uow, 
            IRepository<BlogComment> blogCommentRepository,
            IRepository<Blog> blogRepository) : base(uow)
        {
            _blogCommentRepository = blogCommentRepository;
            _blogRepository = blogRepository;
        }

        public async override Task<CommandResult> Handle(CommentBlogCommand request, CancellationToken cancellationToken)
        {
            var cmdResult = await ValidateCommand(request, new CommentBlogCommandValidator());
            if (!cmdResult.Success)
            {
                return cmdResult;
            }

            var blog = await _blogRepository.GetAsync(request.BlogId);
            if(blog == null)
            {
                return new CommandResult(ResultCode.NOT_FOUND, "该博客不存在");
            }

            BlogComment blogComment = new BlogComment(request.BlogId, request.Content, request.LoginUserId);
            await _blogCommentRepository.InsertAsync(blogComment);
            await UnitOfWork.SaveChangesAsync();
            return new CommandResult();
        }
    }
}
