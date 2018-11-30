using Microsoft.EntityFrameworkCore;
using Npgsql;
using SmartCqrs.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCqrs.Repository.Repositories
{
    public class EfCoreRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>, new()
    {
        private CarMarketDbContext _dbContext;

        public EfCoreRepositoryBase(CarMarketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        public virtual DbSet<TEntity> Table => _dbContext.Set<TEntity>();

        public IQueryable<TEntity> GetAll()
        {
            return GetAllIncluding();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();

            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            return query;
        }

        public List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public TEntity Get(TPrimaryKey id)
        {
            return _dbContext.Find<TEntity>(id);
        }

        public Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return _dbContext.FindAsync<TEntity>(id);
        }

        public TEntity FirstOrDefault(TPrimaryKey id)
        {
            return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public TEntity GetOrCreate(Expression<Func<TEntity, bool>> predicate)
        {
            TEntity entity = FirstOrDefault(predicate);
            if(entity == null)
            {
                entity = new TEntity();
            }
            return entity;
        }

        public async Task<TEntity> GetOrCreateAsync(Expression<Func<TEntity, bool>> predicate)
        {
            TEntity entity = await FirstOrDefaultAsync(predicate);
            if (entity == null)
            {
                entity = new TEntity();
            }
            return entity;
        }


        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public async Task<object> ExecuteScalarAsync(string sql, Dictionary<string,object> parameters = null)
        {
            var connection = _dbContext.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                if(parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        command.Parameters.Add(new NpgsqlParameter(p.Key, p.Value));
                    }
                }
                return await command.ExecuteScalarAsync();
            }
        }

        public TEntity Insert(TEntity entity)
        {
            return Table.Add(entity).Entity;
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity = Update(entity);
            return Task.FromResult(entity);
        }

        public TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient()
                ? Insert(entity)
                : Update(entity);
        }

        public async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return entity.IsTransient()
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }

        public TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            return InsertOrUpdate(entity).Id;
        }

        public Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            return Task.FromResult(InsertOrUpdateAndGetId(entity));
        }

        public void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public void Delete(TPrimaryKey id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            entity = FirstOrDefault(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            //Could not found the entity, do nothing.
        }

        public Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        public Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
            return Task.FromResult(0);
        }

        public int Count()
        {
            return GetAll().Count();
        }

        public async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).Count();
        }

        public async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        public long LongCount()
        {
            return GetAll().LongCount();
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }

        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        public Task EnsureCollectionLoadedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> collectionExpression,
            CancellationToken cancellationToken)
            where TProperty : class
        {
            return _dbContext.Entry(entity).Collection(collectionExpression).LoadAsync(cancellationToken);
        }

        public Task EnsurePropertyLoadedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            CancellationToken cancellationToken)
            where TProperty : class
        {
            return _dbContext.Entry(entity).Reference(propertyExpression).LoadAsync(cancellationToken);
        }

        private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            var entry = _dbContext.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, (ent.Entity as TEntity).Id)
                );

            return entry?.Entity as TEntity;
        }

        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        
    }
}
