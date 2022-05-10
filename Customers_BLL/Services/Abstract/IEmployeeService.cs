using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customers_BLL.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResponse>> GetAllAsync();

        Task<EmployeeResponse> GetByIdAsync(int id);

        Task<IEnumerable<EmployeeResponse>> GetByStatusAsync(string status);

        Task<int> InsertAsync(EmployeeRequest request);

        Task<bool> UpdateAsync(EmployeeRequest request);

        Task DeleteByIdAsync(int id);
    }
}
