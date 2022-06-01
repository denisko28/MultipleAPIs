using System.Threading.Tasks;
using HR_DAL.Entities;

namespace HR_DAL.MongoRepositories.Abstract
{
    public interface IBranchMongoRepository
    {
        Task InsertAsync(Branch branch);

        Task UpdateAsync(Branch branch);

        Task DeleteByIdAsync(int id);
    }
}