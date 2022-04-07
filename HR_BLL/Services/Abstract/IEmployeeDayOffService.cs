using MultipleAPIs.HR_BLL.DTO.Responses;
using MultipleAPIs.HR_BLL.DTO.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultipleAPIs.HR_BLL.Services.Abstract
{
    public interface IEmployeeDayOffService
    {
        Task<IEnumerable<EmployeeDayOffResponse>> GetAllAsync();

        Task<EmployeeDayOffResponse> GetByIdAsync(int Id);

        Task<int> InsertAsync(EmployeeDayOffRequest request);

        Task<bool> UpdateAsync(EmployeeDayOffRequest request);

        Task DeleteByIdAsync(int Id);
    }
}
