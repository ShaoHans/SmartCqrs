using AutoMapper;
using SmartCqrs.Application.Validations;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Enumeration;
using SmartCqrs.Infrastructure.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCqrs.Application.Commands
{
    public class PublishCarCommandHandler : BaseCommandHandler<PublishCarCommand,int>
    {
        private readonly IRepository<Car> _carSourceRepository;
        private readonly IUserRepository _userRepository;
        public PublishCarCommandHandler(IUnitOfWork uow, 
            IRepository<Car> carSourceRepository,
            IUserRepository userRepository) : base(uow)
        {
            _carSourceRepository = carSourceRepository;
            _userRepository = userRepository;
        }

        public override async Task<CommandResult<int>> Handle(PublishCarCommand request, CancellationToken cancellationToken)
        {
            var validateResult = await ValidateCommand(request, new PublishCarCommandValidator());
            if(!validateResult.Success)
            {
                return new CommandResult<int>(validateResult.Code, validateResult.Message);
            }

            Car carSource;

            if (request.PublishMode == PublishMode.New)
            {
                carSource = new Car(request.LoginUserId);
            }
            else
            {
                carSource = await _carSourceRepository.GetAsync(request.CarId);
                if(carSource == null)
                {
                    return new CommandResult<int>(ResultCode.FAIL, "不存在此车辆信息");
                }
            }

            Mapper.Map(request, carSource);

            if(request.PublishMode == PublishMode.New)
            {
                await _carSourceRepository.InsertAsync(carSource);
            }
            else
            {
                if (request.PublishMode == PublishMode.RePublish)
                {
                    carSource.RePublish();
                }
                await _carSourceRepository.UpdateAsync(carSource);
            }

            await Uow.SaveChangesAsync();
            return new CommandResult<int>(data: carSource.Id);
        }
    }
}
