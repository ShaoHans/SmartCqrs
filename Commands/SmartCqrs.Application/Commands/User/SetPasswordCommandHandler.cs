using System.Threading;
using System.Threading.Tasks;
using SmartCqrs.Application.Validations;
using SmartCqrs.Domain.Repositories;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Infrastructure.CommonServices;
using SmartCqrs.Infrastructure.Results;

namespace SmartCqrs.Application.Commands
{
    public class SetPasswordCommandHandler : BaseCommandHandler<SetPasswordCommand>
    {
        private readonly TongHangBrokerCommonServiceClient _client;
        private readonly IUserRepository _userRepository;
        public SetPasswordCommandHandler(IUnitOfWork uow, 
            TongHangBrokerCommonServiceClient client,
            IUserRepository userRepository) : base(uow)
        {
            _client = client;
            _userRepository = userRepository;
        }

        public override async Task<CommandResult> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var validateResult = await ValidateCommand(request, new SetPasswordCommandValidator());
            if (!validateResult.Success)
            {
                return validateResult;
            }

            validateResult = await _client.ValidateSmsCode(request.BussinessType, request.PhoneNo, request.SmsCode);
            if(!validateResult.Success)
            {
                return validateResult;
            }

            var user = await _userRepository.GetByMobileAsync(request.PhoneNo);
            if(user == null)
            {
                return new CommandResult(ResultCode.NOT_FOUND, "不存在此手机号码的用户");
            }

            user.SetPassword(request.Password);
            await _userRepository.UpdateAsync(user);
            await UnitOfWork.SaveChangesAsync();
            return new CommandResult();
        }
    }
}
