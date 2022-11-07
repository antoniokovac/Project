using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.DAL;
using Project.Model.DatabaseModels;
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

        /// <summary>
        /// Returns paged, sorted and filtered list of database entities.
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="filter">Filter expression</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="page">Page number</param>
        /// <param name="sort">Sort expression</param>
        /// <returns></returns>
        public async Task<List<T>> GetAll<T>(Expression<Func<T, bool>> filter, Expression<Func<T, string>> sort, QueryParameters query) where T : class
        {
            var filterSet = dbContext.Set<T>()
                .AsNoTracking()
                .Where(filter);

            var sortedSet = query.SortOrder == SortOrder.Ascending ? filterSet.OrderBy(sort) : filterSet.OrderByDescending(sort);

            var pagedSet = sortedSet.Skip((query.Page -1) * query.PageSize).Take(query.PageSize);


            return await pagedSet.ToListAsync();
        }

        /// <summary>
        /// Returns an entity by id
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned </typeparam>
        /// <param name="id">Entity ID</param>
        /// <returns></returns>
        public async Task<T> Get<T>(Guid id) where T : class
        {
            var vehicle = await dbContext.Set<T>().FindAsync(id);
            return vehicle;
        }

        /// <summary>
        /// Adds entity to database
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="entity">Entity to be added</param>
        /// <returns>Returns true if entity is added, false otherwise</returns>
        public async Task<bool> Create<T>(T entity) where T : class
        {
            var vehicle = await dbContext.Set<T>().AddAsync(entity);

            return vehicle is not null;
        }

        /// <summary>
        /// Updates entity in database
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>Returns true if entity is updated, false otherwise</returns>
        public bool Update<T>(T entity) where T : class
        {
            var vehicle = dbContext.Set<T>().Update(entity);

            return vehicle is not null;
        }

        /// <summary>
        /// Deletes entity in database
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="model">Entity to be deleted</param>
        /// <returns>Returns true if entity is deleted, false otherwise</returns>
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
