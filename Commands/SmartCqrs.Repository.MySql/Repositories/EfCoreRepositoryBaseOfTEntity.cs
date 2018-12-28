using SmartCqrs.Domain.SeedWork;

namespace SmartCqrs.Repository.MySql.Repositories
{
    public class EfCoreRepositoryBase<TEntity> : EfCoreRepositoryBase<TEntity, int>, IRepository<TEntity> where TEntity : Entity<int>, new()
    {
        public EfCoreRepositoryBase(SmartBlogMySqlDbContext dbContext) : base(dbContext)
        {
        }
    }
}
