using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.Repositories;

namespace SmartCqrs.Repository.MySql.Repositories
{
    public class UserAssetRepository : EfCoreRepositoryBase<UserAsset>, IUserAssetRepository
    {
        public UserAssetRepository(SmartBlogMySqlDbContext dbContext) : base(dbContext)
        {
        }

        public UserAsset GetOrCreate(Guid userId)
        {
            UserAsset userAsset = FirstOrDefault(u => u.UserId == userId);
            if (userAsset == null)
            {
                return new UserAsset
                {
                    UserId = userId
                };
            }
            return userAsset;
        }

        public async Task<UserAsset> GetOrCreateAsync(Guid userId)
        {
            UserAsset userAsset = await FirstOrDefaultAsync(u => u.UserId == userId);
            if (userAsset == null)
            {
                return new UserAsset
                {
                    UserId = userId
                };
            }
            return userAsset;
        }
    }
}
