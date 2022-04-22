using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;

namespace HR_BLL.Services.Abstract
{
    public interface IEmployeeDayOffService
    {
        Task<IEnumerable<EmployeeDayOffResponse>> GetAllAsync();

        Task<EmployeeDayOffResponse> GetByIdAsync(int id);

        Task<int> InsertAsync(EmployeeDayOffRequest request);

        Task<bool> UpdateAsync(EmployeeDayOffRequest request);

        Task DeleteByIdAsync(int id);
    }
}
