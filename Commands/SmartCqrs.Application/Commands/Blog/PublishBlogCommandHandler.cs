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
    public class PublishBlogCommandHandler : BaseCommandHandler<PublishBlogCommand, int>
    {
        private readonly IRepository<Blog> _blogRepository;

        public PublishBlogCommandHandler(IUnitOfWork uow,
            IRepository<Blog> blogRepository) : base(uow)
        {
            _blogRepository = blogRepository;
        }

        public override async Task<CommandResult<int>> Handle(PublishBlogCommand request, CancellationToken cancellationToken)
        {
            var validateResult = await ValidateCommand(request, new PublishBlogCommandValidator());
            if(!validateResult.Success)
            {
                return new CommandResult<int>(validateResult.Code, validateResult.Message);
            }

            Blog blog = new Blog();
            blog.Publish(request.Title, request.Content, request.CoverUrl, request.LoginUserId);
            int blogId = await _blogRepository.InsertAndGetIdAsync(blog);
            await UnitOfWork.SaveChangesAsync();
            return new CommandResult<int>(data: blogId);
        }
    }
}
