using SmartCqrs.Infrastructure.Attributes;
using SmartCqrs.Infrastructure.Results;
using MediatR;
using System;

namespace SmartCqrs.Application.Commands
{
    public class Command
    {
        /// <summary>
        /// 当前登录用户Id（前端不需要赋值）
        /// </summary>
        [HideInDocs]
        public Guid LoginUserId { get; set; }
    }

    public class BaseCommand<T> : Command, IRequest<CommandResult<T>>
    {

    }

    public class BaseCommand : Command, IRequest<CommandResult>
    {
    }
}
