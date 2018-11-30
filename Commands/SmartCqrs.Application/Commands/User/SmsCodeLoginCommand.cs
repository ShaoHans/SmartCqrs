using SmartCqrs.Application.Dtos;

namespace SmartCqrs.Application.Commands
{
    public class SmsCodeLoginCommand : BaseCommand<LoginDto>
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        public string SmsCode { get; set; }
    }
}
