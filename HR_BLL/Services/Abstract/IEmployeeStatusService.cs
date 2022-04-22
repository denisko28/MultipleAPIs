using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;

namespace HR_BLL.Services.Abstract
{
    public interface IEmployeeStatusService
    {
        Task<IEnumerable<EmployeeStatusResponse>> GetAllAsync();

        Task<EmployeeStatusResponse> GetByIdAsync(int id);

        Task<int> InsertAsync(EmployeeStatusRequest request);

        Task<bool> UpdateAsync(EmployeeStatusRequest request);

        Task DeleteByIdAsync(int id);
    }
}
