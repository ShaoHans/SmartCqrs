using SmartCqrs.Domain.SeedWork;

namespace SmartCqrs.Repository.Repositories
{
    public class EfCoreRepositoryBase<TEntity> : EfCoreRepositoryBase<TEntity, int>, IRepository<TEntity> where TEntity : Entity<int>, new()
    {
        public EfCoreRepositoryBase(CarMarketDbContext dbContext) : base(dbContext)
        {
        }
    }
}
