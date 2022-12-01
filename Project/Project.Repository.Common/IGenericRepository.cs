using Project.Common;
using System.Linq.Expressions;

namespace Project.Repository.Common
{
    public interface IGenericRepository
    {

        /// <summary>
        /// Returns paged, sorted and filtered list of database entities.
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="filter">Filter expression</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="page">Page number</param>
        /// <param name="sort">Sort expression</param>
        /// <returns></returns>
        public Task<List<T>> GetAll<T>(Expression<Func<T, bool>> filter, Expression<Func<T, string>> sort, QueryParameters query) where T : class;


        /// <summary>
        /// Returns an entity by id
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned </typeparam>
        /// <param name="id">Entity ID</param>
        /// <returns></returns>
        public Task<T> Get<T>(Guid id) where T : class;

        /// <summary>
        /// Adds entity to database
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="entity">Entity to be added</param>
        /// <returns>Returns true if entity is added, false otherwise</returns>
        public Task<bool> Create<T>(T entity) where T : class;

        /// <summary>
        /// Updates entity in database
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="entity">Entity to be updated</param>
        /// <returns>Returns true if entity is updated, false otherwise</returns>
        public bool Update<T>(T entity) where T : class;

        /// <summary>
        /// Deletes entity in database
        /// </summary>
        /// <typeparam name="T">Type of entity that will be returned</typeparam>
        /// <param name="model">Entity to be deleted</param>
        /// <returns>Returns true if entity is deleted, false otherwise</returns>
        public bool Delete<T>(T model) where T : class;

        public Task<int> SaveChangesAsync();
    }

}
