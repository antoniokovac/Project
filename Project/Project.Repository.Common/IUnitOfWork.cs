using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public interface IUnitOfWork 
    {
        Task SaveChangesAsync();
        Task<bool> AddAsync<T>(T entity) where T : class;
        bool Update<T>(T entity) where T : class;
        bool Delete<T>(T entity) where T : class;

    }
}
