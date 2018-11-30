using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Infrastructure.Results;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCqrs.Application.Commands
{
    public abstract class BaseCommandHandler<TCommand, TData> : BaseCommandHandler, IRequestHandler<TCommand, CommandResult<TData>> where TCommand : BaseCommand<TData>
    {
        public BaseCommandHandler(IUnitOfWork uow) : base(uow)
        {
        }

        public abstract Task<CommandResult<TData>> Handle(TCommand request, CancellationToken cancellationToken);
    }

    public abstract class BaseCommandHandler<TCommand> : BaseCommandHandler, IRequestHandler<TCommand, CommandResult> where TCommand : BaseCommand
    {
        public BaseCommandHandler(IUnitOfWork uow) : base(uow)
        {
        }

        public abstract Task<CommandResult> Handle(TCommand request, CancellationToken cancellationToken);
    }

    public abstract class BaseCommandHandler
    {
        public IUnitOfWork Uow { get; }

        public BaseCommandHandler(IUnitOfWork uow)
        {
            Uow = uow;
        }

        public async Task<CommandResult> ValidateCommand<TCommand>(TCommand request, AbstractValidator<TCommand> validator)
        {
            // 参数校验
            ValidationResult validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return new CommandResult(ResultCode.FAIL, string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage)));
            }
            return new CommandResult();
        }
    }
}
