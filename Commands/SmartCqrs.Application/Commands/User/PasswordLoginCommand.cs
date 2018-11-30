using SmartCqrs.Application.Dtos;

namespace SmartCqrs.Application.Commands
{
    public class PasswordLoginCommand : BaseCommand<LoginDto>
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
