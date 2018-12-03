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
    public class PasswordLoginCommandHandler : BaseCommandHandler<PasswordLoginCommand, LoginDto>
    {
        private readonly TongHangBrokerAuthServiceClient _authServiceClient;
        private readonly IUserRepository _userRepository;

        public PasswordLoginCommandHandler(IUnitOfWork uow,
            TongHangBrokerAuthServiceClient authServiceClient,
            IUserRepository userRepository) : base(uow)
        {
            _authServiceClient = authServiceClient;
            _userRepository = userRepository;
        }

        public async override Task<CommandResult<LoginDto>> Handle(PasswordLoginCommand request, CancellationToken cancellationToken)
        {
            var cmdResult = await ValidateCommand(request, new PasswordLoginCommandValidator());
            if(!cmdResult.Success)
            {
                return new CommandResult<LoginDto>(cmdResult.Code, cmdResult.Message);
            }

            User user = await _userRepository.GetByMobileAsync(request.Mobile);
            if(user == null || !user.ValidatePassword(request.Password))
            {
                return new CommandResult<LoginDto>(ResultCode.FAIL, "手机号或密码错误");
            }

            if(!user.CanLogin())
            {
                return new CommandResult<LoginDto>(ResultCode.FAIL, "禁止登录");
            }

            // 调用授权服务获取用户token，返回给客户端
            var tokenResult = await _authServiceClient.GetUserToken(user.Id, user.UserId, user.NickName, user.Mobile, user.Status.ToInt32());
            if(!tokenResult.Success)
            {
                return new CommandResult<LoginDto>(tokenResult.Code, tokenResult.Message);
            }

            user.Login();
            await _userRepository.UpdateAsync(user);
            await UnitOfWork.SaveChangesAsync();
            return new CommandResult<LoginDto>(data: new LoginDto { IsNewRegister = false, AccessToken = tokenResult.Data });
        }
    }
}
