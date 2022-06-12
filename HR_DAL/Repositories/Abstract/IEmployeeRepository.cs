using System.Collections.Generic;
using System.Threading.Tasks;
using HR_DAL.Entities;

namespace HR_DAL.Repositories.Abstract
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetByBranchId(int branchId);
        
        Task<IEnumerable<Employee>> GetByStatusIdAsync(int statusId);
    }
}
