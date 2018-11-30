using SmartCqrs.Infrastructure.Auth;
using SmartCqrs.Infrastructure.Log;
using SmartCqrs.Infrastructure.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartCqrs.Infrastructure.CommonServices
{
    public class TongHangBrokerCommonServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerManager _loggerManager;
        private (string headerName, string token) _token;

        public TongHangBrokerCommonServiceClient(HttpClient httpClient, 
            ILoggerManager loggerManager,
            IServiceAuthorization serviceAuthorization)
        {
            _httpClient = httpClient;
            _loggerManager = loggerManager;

            _token = serviceAuthorization.Token;
            _httpClient.DefaultRequestHeaders.Add(_token.headerName, _token.token);
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="bussinessType">5-动态短信密码</param>
        /// <param name="phoneNo">手机号码</param>
        /// <returns></returns>
        public async Task<CommandResult<CaptchaResponseDto>> SendSmsCode(int bussinessType, string phoneNo)
        {
            var paramList = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("bussinesstype", bussinessType.ToString()),
                new KeyValuePair<string, string>("phoneno", phoneNo)
            };

            try
            {
                var response = await _httpClient.PostAsync("captcha", new FormUrlEncodedContent(paramList));
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CommandResult<CaptchaResponseDto>>(content);
            }
            catch (Exception ex)
            {
                _loggerManager.Error("发送短信出现异常", ex);
                return new CommandResult<CaptchaResponseDto>(ResultCode.FAIL, "发送短信出现异常");
            }
        }

        /// <summary>
        /// 对短信验证码进行校验
        /// </summary>
        /// <param name="bussinessType">5-动态短信密码</param>
        /// <param name="phoneNo">手机号码</param>
        /// <param name="code">短信验证码</param>
        /// <returns></returns>
        public async Task<CommandResult> ValidateSmsCode(int bussinessType, string phoneNo, string code)
        {
            var paramList = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("bussinesstype", bussinessType.ToString()),
                new KeyValuePair<string, string>("phoneno", phoneNo),
                new KeyValuePair<string, string>("code", code),
            };

            try
            {
                var response = await _httpClient.PostAsync("captcha/check", new FormUrlEncodedContent(paramList));
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CommandResult>(content);
            }
            catch (Exception ex)
            {
                _loggerManager.Error("对短信验证码进行校验时出现异常", ex);
                return new CommandResult(ResultCode.FAIL, "对短信验证码进行校验时出现异常");
            }
        }
    }
}
