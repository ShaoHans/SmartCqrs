using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Infrastructure.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCqrs.Application.Commands
{
    public class ViewCarCommandHandler : BaseCommandHandler<ViewCarCommand>
    {
        private readonly IRepository<Car> _carSourceRepository;
        private readonly IRepository<ViewCarLog> _viewCarLogRepository;

        public ViewCarCommandHandler(IUnitOfWork uow, 
            IRepository<Car> carSourceRepository,
            IRepository<ViewCarLog> viewCarLogRepository) : base(uow)
        {
            _carSourceRepository = carSourceRepository;
            _viewCarLogRepository = viewCarLogRepository;
        }

        public override async Task<CommandResult> Handle(ViewCarCommand request, CancellationToken cancellationToken)
        {
            ViewCarLog viewCarRecord = new ViewCarLog
            {
                CarId = request.CarId,
                Ip = request.Ip,
                UserId = request.LoginUserId,
                CreatedTime = DateTime.Now
            };
            viewCarRecord.View();
            await _viewCarLogRepository.InsertAsync(viewCarRecord);
            await Uow.SaveChangesAsync();
            return new CommandResult();
        }
    }
}
