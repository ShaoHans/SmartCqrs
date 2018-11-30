using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

namespace SmartCqrs.Infrastructure.Auth
{
    public class ServiceAuthorization : IServiceAuthorization
    {
        public ServiceAuthorization() { }
        /// <summary>
        /// 获取服务授权
        /// </summary>
        /// <param name="url">授权地址</param>
        /// <param name="clientId">客户端ID</param>
        /// <param name="clientSecret">密钥</param>
        public ServiceAuthorization(string url, string clientId, string clientSecret)
        {
            parameter.url = url;
            parameter.clientId = clientId;
            parameter.clientSecret = clientSecret;
        }
        private (string url, string clientId, string clientSecret) parameter;
        private (string headerName, string token) _Token { get; set; }
        public (string headerName, string token) Token
        {
            get
            {
                if (_Token == default || !JWTHelper.Validate(_Token.token, payLoad =>
                  {
                      return true;
                  }))
                {
                    _Token = GetToken(parameter.url, parameter.clientId, parameter.clientSecret);
                }
                return _Token;
            }

            set
            {
                _Token = value;
            }
        }

        /// <summary>
        /// 请求服务授权
        /// </summary>
        /// <param name="url">服务授权地址</param>
        /// <param name="clientId">客户端ID</param>
        /// <param name="clientSecret">密钥</param>
        /// <returns></returns>
        private (string headerName, string token) GetToken(string url, string clientId, string clientSecret)
        {
            Debug.WriteLine("GetToken INIT");
            HttpClient httpClient = new HttpClient();
            string token;
            List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("appId", clientId),
                new KeyValuePair<string, string>("appSecret", clientSecret)
            };
            try
            {
                var result = httpClient.PostAsync(url, new FormUrlEncodedContent(paramList)).Result;
                token = result.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                httpClient.Dispose();
            }
            JObject jo = (JObject)JsonConvert.DeserializeObject(token);
            return (headerName: "access-token", token: jo["token"].ToString());
        }
    }

    public interface IServiceAuthorization
    {
        (string headerName, string token) Token { get; }
    }
}
