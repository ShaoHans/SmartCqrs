﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Infrastructure.Results;

namespace SmartCqrs.Application.Commands
{
    public class CollectBlogCommandHandler : BaseCommandHandler<CollectBlogCommand>
    {
        private readonly IRepository<BlogCollect> _blogCollectRepository;
        private readonly IRepository<Blog> _blogRepository;

        public CollectBlogCommandHandler(IUnitOfWork uow,
            IRepository<BlogCollect> blogCollectRepository,
            IRepository<Blog> blogRepository) : base(uow)
        {
            _blogCollectRepository = blogCollectRepository;
            _blogRepository = blogRepository;
        }


        public override async Task<CommandResult> Handle(CollectBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _blogRepository.GetAsync(request.BlogId);
            if(blog == null)
            {
                return new CommandResult(ResultCode.NOT_FOUND, "该博客不存在");
            }

            var blogCollect = new BlogCollect(request.BlogId, request.LoginUserId);
            await _blogCollectRepository.InsertAsync(blogCollect);
            return new CommandResult();
        }
    }
}
