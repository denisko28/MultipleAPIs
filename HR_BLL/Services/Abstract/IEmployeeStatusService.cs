using MultipleAPIs.HR_BLL.DTO.Responses;
using MultipleAPIs.HR_BLL.DTO.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultipleAPIs.HR_BLL.Services.Abstract
{
    public interface IEmployeeStatusService
    {
        Task<IEnumerable<EmployeeStatusResponse>> GetAllAsync();

        Task<EmployeeStatusResponse> GetByIdAsync(int Id);

        Task<int> InsertAsync(EmployeeStatusRequest request);

        Task<bool> UpdateAsync(EmployeeStatusRequest request);

        Task DeleteByIdAsync(int Id);
    }
}
