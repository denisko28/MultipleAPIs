using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customers_DAL.Repositories.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> GetCompleteEntityAsync(int id);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteByIdAsync(int id);
    }
}
