using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultipleAPIs.HR_DAL.Repositories.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int Id);

        Task<int> InsertAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task DeleteByIdAsync(int Id);
    }
}
