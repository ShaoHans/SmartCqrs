using System.Threading.Tasks;

namespace SmartCqrs.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        Task SaveChangesAsync();

    }
}
