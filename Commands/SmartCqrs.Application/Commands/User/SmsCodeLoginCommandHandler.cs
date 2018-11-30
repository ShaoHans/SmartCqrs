using System;
using System.Threading;
using System.Threading.Tasks;
using SmartCqrs.Application.Dtos;
using SmartCqrs.Application.Validations;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Infrastructure.CommonServices;
using SmartCqrs.Infrastructure.Results;

namespace SmartCqrs.Application.Commands
{
    public class SmsCodeLoginCommandHandler : BaseCommandHandler<SmsCodeLoginCommand, LoginDto>
    {
        private readonly TongHangBrokerAuthServiceClient _authServiceClient;
        private readonly TongHangBrokerCommonServiceClient _commonServiceClient;
        private readonly IUserRepository _userRepository;

        public SmsCodeLoginCommandHandler(IUnitOfWork uow,
            TongHangBrokerAuthServiceClient authServiceClient,
            TongHangBrokerCommonServiceClient commonServiceClient,
            IUserRepository userRepository) : base(uow)
        {
            _authServiceClient = authServiceClient;
            _commonServiceClient = commonServiceClient;
            _userRepository = userRepository;
        }

        public async override Task<CommandResult<LoginDto>> Handle(SmsCodeLoginCommand request, CancellationToken cancellationToken)
        {
            // 参数检查
            var cmdResult = await ValidateCommand(request, new SmsCodeLoginCommandValidator());
            if(!cmdResult.Success)
            {
                return new CommandResult<LoginDto>(cmdResult.Code, cmdResult.Message);
            }

            // 调用公共服务接口对验证码进行校验
            cmdResult = await _commonServiceClient.ValidateSmsCode(5, request.Mobile, request.SmsCode);
            if(!cmdResult.Success)
            {
                return new CommandResult<LoginDto>(cmdResult.Code, cmdResult.Message);
            }

            LoginDto loginDto = new LoginDto();
            User user = await _userRepository.GetByMobileAsync(request.Mobile);
            if(user == null)
            {
                // 注册
                user = new User();
                user.Regist(request.Mobile);
                user.Login();
                await _userRepository.InsertAndGetIdAsync(user);

                loginDto.IsNewRegister = true;
            }
            else
            {
                if (!user.CanLogin())
                {
                    return new CommandResult<LoginDto>(ResultCode.FAIL, "禁止登录");
                }

                user.Login();
                await _userRepository.UpdateAsync(user);
            }

            // 调用授权服务获取用户token，返回给客户端
            var tokenResult = await _authServiceClient.GetUserToken(user.Id, user.UserId, user.NickName, user.Mobile, user.Status.ToInt32());
            if (!tokenResult.Success)
            {
                return new CommandResult<LoginDto>(tokenResult.Code, tokenResult.Message);
            }

            await Uow.SaveChangesAsync();
            loginDto.AccessToken = tokenResult.Data;
            return new CommandResult<LoginDto>(data: loginDto);
        }
    }
}
