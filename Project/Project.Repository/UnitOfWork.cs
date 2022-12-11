using Project.Repository.Common;
using System.Transactions;

namespace Project.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IGenericRepository repository;
        private readonly TransactionScope scope;
        public UnitOfWork(IGenericRepository repository)
        {
            this.repository = repository;
            scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        public Task<bool> AddAsync<T>(T entity) where T : class
        {
            return repository.CreateAsync<T>(entity);
        }

        public bool Delete<T>(T entity) where T : class
        {
            return repository.Delete<T>(entity);
        }

        public bool Update<T>(T entity) where T : class
        {
            return repository.Update<T>(entity);
        }

        public async Task SaveChangesAsync()
        {
            await repository.SaveChangesAsync();
            scope.Complete();
        }

        public void Dispose()
        {
            scope.Dispose();
        }
    }
}
