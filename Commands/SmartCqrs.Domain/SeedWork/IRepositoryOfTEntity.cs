namespace SmartCqrs.Domain.SeedWork
{
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : Entity<int>, new()
    {
    }
}
