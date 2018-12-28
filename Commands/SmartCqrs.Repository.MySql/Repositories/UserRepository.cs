using System;
using System.Threading.Tasks;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;

namespace SmartCqrs.Repository.MySql.Repositories
{
    public class UserRepository : EfCoreRepositoryBase<User>, IUserRepository
    {
        public UserRepository(SmartBlogMySqlDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        /// <param name="mobile"></param>
        public async Task<User> GetByMobileAsync(string mobile)
        {
            return await FirstOrDefaultAsync(u => u.Mobile == mobile);
        }

        /// <summary>
        /// 根据用户Id获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User> GetByUserIdAsync(Guid userId)
        {
            return await FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
