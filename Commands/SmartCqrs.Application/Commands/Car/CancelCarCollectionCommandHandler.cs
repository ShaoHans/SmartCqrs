using System;
using System.Threading;
using System.Threading.Tasks;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Infrastructure.Results;

namespace SmartCqrs.Application.Commands
{
    public class CancelCarCollectionCommandHandler : BaseCommandHandler<CancelCarCollectionCommand>
    {
        private readonly IRepository<CollectCar> _collectCarRepository;
        public CancelCarCollectionCommandHandler(IUnitOfWork uow,
            IRepository<CollectCar> collectCarRepository) : base(uow)
        {
            _collectCarRepository = collectCarRepository;
        }

        public override async Task<CommandResult> Handle(CancelCarCollectionCommand request, CancellationToken cancellationToken)
        {
            var collectCar = await _collectCarRepository.FirstOrDefaultAsync(c => c.CarId == request.CarId && c.UserId == request.LoginUserId);
            if (collectCar == null)
            {
                return new CommandResult(ResultCode.FAIL, "您未收藏过该车辆");
            }
            collectCar.CancelCollect();
            await _collectCarRepository.DeleteAsync(collectCar);
            await Uow.SaveChangesAsync();
            return new CommandResult();
        }
    }
}
