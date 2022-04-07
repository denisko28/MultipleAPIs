using MultipleAPIs.HR_BLL.DTO.Responses;
using MultipleAPIs.HR_BLL.DTO.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultipleAPIs.HR_BLL.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResponse>> GetAllAsync();

        Task<EmployeeResponse> GetByIdAsync(int Id);

        Task<IEnumerable<EmployeeResponse>> GetByStatusAsync(string status);

        Task<int> InsertAsync(EmployeeRequest request);

        Task<bool> UpdateAsync(EmployeeRequest request);

        Task DeleteByIdAsync(int Id);
    }
}
