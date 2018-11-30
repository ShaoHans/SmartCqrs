using SmartCqrs.Domain.Models;
using SmartCqrs.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace SmartCqrs.Domain.Repositories
{
    public interface IUserAssetRepository : IRepository<UserAsset>
    {
        UserAsset GetOrCreate(Guid userId);

        Task<UserAsset> GetOrCreateAsync(Guid userId);
    }
}
