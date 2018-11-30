using SmartCqrs.Infrastructure.Log;
using SmartCqrs.Infrastructure.Results;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartCqrs.Infrastructure.CommonServices
{
    public class TongHangBrokerAuthServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerManager _loggerManager;
        private readonly IConfiguration _configuration;
        public TongHangBrokerAuthServiceClient(HttpClient httpClient,
            ILoggerManager loggerManager,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _loggerManager = loggerManager;
            _configuration = configuration;
        }

        /// <summary>
        /// 获取用户token
        /// </summary>
        /// <param name="id">用户编号id</param>
        /// <param name="userId">用户uuid</param>
        /// <param name="nickName">昵称</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="userStatus">用户状态</param>
        /// <returns></returns>
        public async Task<CommandResult> GetUserToken(int id, Guid userId, string nickName, string mobile, int userStatus)
        {
            var paramList = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", id.ToString()),
                new KeyValuePair<string, string>("nickname", nickName),
                new KeyValuePair<string, string>("uuid", userId.ToString()),
                new KeyValuePair<string, string>("mobile", mobile),
                new KeyValuePair<string, string>("usertype", "3"),
                new KeyValuePair<string, string>("isvalid", userStatus.ToString()),
                new KeyValuePair<string, string>("appId", _configuration.GetSection("IdentityServer:ClientId").Value),
                new KeyValuePair<string, string>("appSecret", _configuration.GetSection("IdentityServer:ClientSecret").Value),
            };

            try
            {
                var response = await _httpClient.PostAsync("user/userToken", new FormUrlEncodedContent(paramList));
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CommandResult>(content);
            }
            catch (Exception ex)
            {
                _loggerManager.Error("获取用户token出现异常", ex);
                return new CommandResult(ResultCode.FAIL, "获取用户token失败");
            }
        }

        /// <summary>
        /// 刷新用户token
        /// </summary>
        /// <param name="oldToken"></param>
        /// <returns></returns>
        public async Task<CommandResult> RefreshUserToken(string oldToken)
        {
            var paramList = new List<KeyValuePair<string, string>>
            {
            };

            try
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", oldToken);
                var response = await _httpClient.PostAsync("user/refresh", new FormUrlEncodedContent(paramList));
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CommandResult>(content);
            }
            catch (Exception ex)
            {
                _loggerManager.Error("刷新用户token出现异常", ex);
                return new CommandResult(ResultCode.FAIL, "刷新用户token失败");
            }
        }
    }
}
