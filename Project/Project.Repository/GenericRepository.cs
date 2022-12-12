using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.DAL;
using Project.Repository.Common;
using System.Linq.Expressions;

namespace Project.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly VehicleDbContext dbContext;

        public GenericRepository(VehicleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<T>> GetAll<T>(Expression<Func<T, bool>> filter, Expression<Func<T, string>> sort, QueryParameters query) where T : class
        {
            var filterSet = dbContext.Set<T>()
                .AsNoTracking()
                .Where(filter);

            var sortedSet = query.SortOrder == SortOrder.Ascending ? filterSet.OrderBy(sort) : filterSet.OrderByDescending(sort);
            var pagedSet = sortedSet.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize);

            return await pagedSet.ToListAsync();
        }

        public async Task<T> GetAsync<T>(Guid id) where T : class
        {
            var vehicle = await dbContext.Set<T>().FindAsync(id);
            return vehicle;
        }

        public async Task<bool> CreateAsync<T>(T entity) where T : class
        {
            var vehicle = await dbContext.Set<T>().AddAsync(entity);

            return vehicle is not null;
        }

        public bool Update<T>(T entity) where T : class
        {
            var vehicle = dbContext.Set<T>().Update(entity);

            return vehicle is not null;
        }

        public bool Delete<T>(T model) where T : class
        {
            var vehicle = dbContext.Set<T>().Remove(model);

            return vehicle is not null;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
