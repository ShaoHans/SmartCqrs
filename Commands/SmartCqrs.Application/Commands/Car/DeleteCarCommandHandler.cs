using System.Threading;
using System.Threading.Tasks;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Enumeration;
using SmartCqrs.Infrastructure.Results;

namespace SmartCqrs.Application.Commands
{
    public class DeleteCarCommandHandler : BaseCommandHandler<DeleteCarCommand>
    {
        private readonly IRepository<Car> _carSourceRepository;
        public DeleteCarCommandHandler(IUnitOfWork uow, IRepository<Car> carSourceRepository) : base(uow)
        {
            _carSourceRepository = carSourceRepository;
        }

        public override async Task<CommandResult> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var carSource = await _carSourceRepository.GetAsync(request.CarSourceId);
            if(carSource == null)
            {
                return new CommandResult(ResultCode.FAIL, "不存在此车辆");
            }

            if (!carSource.UserId.Equals(request.LoginUserId))
            {
                return new CommandResult(ResultCode.FAIL, "不能删除别人发布的车辆");
            }

            if(carSource.Status == CarStatus.Deleted)
            {
                return new CommandResult(ResultCode.FAIL, "此车辆已删除");
            }

            carSource.Delete();
            await _carSourceRepository.UpdateAsync(carSource);
            await Uow.SaveChangesAsync();
            return new CommandResult();
        }
    }
}
