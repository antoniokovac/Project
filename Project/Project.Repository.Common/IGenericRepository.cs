using Project.Common;
using System.Linq.Expressions;

namespace Project.Repository.Common
{
    public interface IGenericRepository
    {
        public Task<List<T>> GetAll<T>(Expression<Func<T, bool>> filter, Expression<Func<T, string>> sort, QueryParameters query) where T : class;

        public Task<T> GetAsync<T>(Guid id) where T : class;

        public Task<bool> CreateAsync<T>(T entity) where T : class;

        public bool Update<T>(T entity) where T : class;

        public bool Delete<T>(T model) where T : class;

        public Task<int> SaveChangesAsync();
    }

}
