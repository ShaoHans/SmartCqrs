using System.Collections.Generic;

namespace SmartCqrs.Application.Dtos
{
    public class UpdateUserInfoDto
    {
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

        /// <summary>
        /// 背景图
        /// </summary>
        public string BgImgUrl { get; set; }
    }
}
