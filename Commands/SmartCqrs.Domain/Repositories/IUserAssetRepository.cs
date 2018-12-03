using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;

namespace SmartCqrs.Domain.Repositories
{
    public interface IUserAssetRepository : IRepository<UserAsset>
    {
        UserAsset GetOrCreate(Guid userId);

        Task<UserAsset> GetOrCreateAsync(Guid userId);
    }
}
