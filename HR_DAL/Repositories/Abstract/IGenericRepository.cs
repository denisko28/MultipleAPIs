using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_DAL.Repositories.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<int> InsertAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task DeleteByIdAsync(int id);
    }
}
