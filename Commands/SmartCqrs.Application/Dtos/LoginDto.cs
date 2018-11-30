namespace SmartCqrs.Application.Dtos
{
    public class LoginDto
    {
        /// <summary>
        /// 是否为新注册用户（true：是，false：否）
        /// </summary>
        public bool IsNewRegister { get; set; }

        /// <summary>
        /// 用户token
        /// </summary>
        public string AccessToken { get; set; }
    }
}
