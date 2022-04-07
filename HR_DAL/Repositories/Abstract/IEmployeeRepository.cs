using MultipleAPIs.HR_DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultipleAPIs.HR_DAL.Repositories.Abstract
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetByStatusIdAsync(int StatusId);
    }
}
