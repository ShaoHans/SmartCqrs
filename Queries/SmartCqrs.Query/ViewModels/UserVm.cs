using SmartCqrs.Enumeration;
using Newtonsoft.Json;
using System;

namespace SmartCqrs.Query.ViewModels
{
    public class UserVm
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像Url
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 所在城市名称
        /// </summary>
        public string CityName { get; set; }
    }
}
