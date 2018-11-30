using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace SmartCqrs.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        /// <param name="mobile"></param>
        Task<User> GetByMobileAsync(string mobile);

        /// <summary>
        /// 根据用户Id获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> GetByUserIdAsync(Guid userId);
    }
}
