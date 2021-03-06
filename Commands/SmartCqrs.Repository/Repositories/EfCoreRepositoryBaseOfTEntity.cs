﻿using SmartCqrs.Domain.SeedWork;

namespace SmartCqrs.Repository.Postgresql.Repositories
{
    public class EfCoreRepositoryBase<TEntity> : EfCoreRepositoryBase<TEntity, int>, IRepository<TEntity> where TEntity : Entity<int>, new()
    {
        public EfCoreRepositoryBase(SmartBlogPostgresqlDbContext dbContext) : base(dbContext)
        {
        }
    }
}
