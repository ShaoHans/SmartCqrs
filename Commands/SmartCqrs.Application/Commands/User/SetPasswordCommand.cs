namespace SmartCqrs.Application.Commands
{
    public class SetPasswordCommand : BaseCommand
    {
        /// <summary>
        /// 业务类别：5-动态短信密码
        /// </summary>
        public int BussinessType { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNo { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        public string SmsCode { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
    }
}
