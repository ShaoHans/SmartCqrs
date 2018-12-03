using SmartCqrs.Domain.Events;
using SmartCqrs.Domain.SeedWork;
using SmartCqrs.Enumeration;
using System;
using System.ComponentModel;

namespace SmartCqrs.Domain.Models
{
    /// <summary>
    /// 用户基本信息
    /// </summary>
    public class User : Entity
    {
        #region 属性

        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像Url
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 所在城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 注册渠道（0：App注册，1：后台手动添加，2：Web注册）
        /// </summary>
        public RegisterChannel RegisterChannel { get; set; }

        /// <summary>
        /// 最近一次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 用户状态（0：有效，9：已删除）
        /// </summary>
        public UserStatus Status { get; set; }

        

        #endregion

        #region 领域方法

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="mobile"></param>
        public void Regist(string mobile)
        {
            UserId = Guid.NewGuid();
            Mobile = mobile;
            RegisterChannel = RegisterChannel.App;
            RegisterTime = DateTime.Now;
            Status = UserStatus.Actived;

            // 新注册用户添加积分
            AddDomainEvent(new UserPointTaskHappenedDomainEvent(UserId, PointTaskType.Registed));
        } 

        /// <summary>
        /// 登录
        /// </summary>
        public void Login()
        {
            LastLoginTime = DateTime.Now;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="plainPwd"></param>
        public void SetPassword(string plainPwd)
        {
            // 没加盐！！！
            Password = plainPwd.MD5Hash();
        }

        /// <summary>
        /// 检查密码是否正确
        /// </summary>
        /// <param name="plainPwd"></param>
        /// <returns></returns>
        public bool ValidatePassword(string plainPwd)
        {
            return plainPwd.MD5Hash().Equals(Password, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 检查能否登录
        /// </summary>
        /// <returns></returns>
        public bool CanLogin()
        {
            return Status != UserStatus.Deleted;
        }

        #endregion
    }
}
