using System;
using System.Threading;
using System.Threading.Tasks;
using SmartCqrs.Application.Validations;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Infrastructure.Results;

namespace SmartCqrs.Application.Commands
{
    public class CollectCarCommandHandler : BaseCommandHandler<CollectCarCommand>
    {
        private readonly IRepository<Car> _carSourceRepository;
        private readonly IRepository<CollectCar> _collectCarRepository;
        public CollectCarCommandHandler(IUnitOfWork uow, 
            IRepository<Car> carSourceRepository,
            IRepository<CollectCar> collectCarRepository) : base(uow)
        {
            _carSourceRepository = carSourceRepository;
            _collectCarRepository = collectCarRepository;
        }

        public override async Task<CommandResult> Handle(CollectCarCommand request, CancellationToken cancellationToken)
        {
            var validateResult = await ValidateCommand(request, new CollectCaCommandValidator());
            if(!validateResult.Success)
            {
                return validateResult;
            }

            var count = await _collectCarRepository.CountAsync(c => c.CarId == request.CarId && c.UserId == request.LoginUserId);
            if (count > 0)
            {
                return new CommandResult(ResultCode.FAIL, "您已收藏过该车辆");
            }

            CollectCar collectCar = new CollectCar
            {
                CarId = request.CarId,
                UserId = request.LoginUserId,
                CollectedTime = DateTime.Now
            };
            collectCar.Collect();
            await _collectCarRepository.InsertAsync(collectCar);
            await Uow.SaveChangesAsync();
            return new CommandResult();
        }
    }
}
