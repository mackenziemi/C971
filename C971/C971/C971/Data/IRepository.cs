using System.Collections.Generic;
using System.Threading.Tasks;

namespace C971.Data
{
    public interface IRepository<T>
        where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> InsertAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
    }
}

