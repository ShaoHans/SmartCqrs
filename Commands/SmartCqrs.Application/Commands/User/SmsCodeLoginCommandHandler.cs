using System;
using System.Threading;
using System.Threading.Tasks;
using SmartCqrs.Application.Dtos;
using SmartCqrs.Application.Validations;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Infrastructure.Auth;
using SmartCqrs.Infrastructure.CommonServices;
using SmartCqrs.Infrastructure.Results;

namespace SmartCqrs.Application.Commands
{
    public class SmsCodeLoginCommandHandler : BaseCommandHandler<SmsCodeLoginCommand, LoginDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        public SmsCodeLoginCommandHandler(IUnitOfWork uow,
            IUserRepository userRepository,
            IJwtService jwtService) : base(uow)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
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
            //cmdResult = await _commonServiceClient.ValidateSmsCode(5, request.Mobile, request.SmsCode);
            //if(!cmdResult.Success)
            //{
            //    return new CommandResult<LoginDto>(cmdResult.Code, cmdResult.Message);
            //}

            LoginDto loginDto = new LoginDto();
            User user = await _userRepository.GetByMobileAsync(request.Mobile);
            if(user == null)
            {
                // 注册
                user = new User();
                user.Regist(request.Mobile);
                user.Login();
                await _userRepository.InsertAsync(user);

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
            //var tokenResult = await _authServiceClient.GetUserToken(user.Id, user.UserId, user.NickName, user.Mobile, user.Status.ToInt32());
            //if (!tokenResult.Success)
            //{
            //    return new CommandResult<LoginDto>(tokenResult.Code, tokenResult.Message);
            //}

            await UnitOfWork.SaveChangesAsync();
            loginDto.AccessToken = _jwtService.GenerateToken(new ClientUser { Sub = user.Mobile, UUID = user.UserId ,Nickname = user.NickName});
            return new CommandResult<LoginDto>(data: loginDto);
        }
    }
}
