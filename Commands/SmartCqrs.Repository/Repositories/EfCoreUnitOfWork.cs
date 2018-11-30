using MediatR;
using SmartCqrs.Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace SmartCqrs.Repository.Repositories
{
    public class EfCoreUnitOfWork : IUnitOfWork, IDisposable
    {
        private CarMarketDbContext _dbContext;
        private IMediator _mediator;

        public EfCoreUnitOfWork()
        {
        }

        public EfCoreUnitOfWork(CarMarketDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        private bool _isCommitted = false;

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }

        public void SaveChanges()
        {
            if(!_isCommitted)
            {
                _mediator.DispatchDomainEventsAsync(_dbContext).Wait();
                _dbContext.SaveChanges();
                _isCommitted = true;
            }
        }

        public async Task SaveChangesAsync()
        {
            if(!_isCommitted)
            {
                await _mediator.DispatchDomainEventsAsync(_dbContext);
                await _dbContext.SaveChangesAsync();
                _isCommitted = true;
            }
        }
    }
}
