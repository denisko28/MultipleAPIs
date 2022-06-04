using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;

namespace HR_BLL.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResponse>> GetAllAsync();

        Task<EmployeeResponse> GetByIdAsync(int id);

        Task<IEnumerable<EmployeeResponse>> GetByStatusAsync(string status);

        Task<int> InsertAsync(EmployeeRequest request);

        Task<bool> UpdateAsync(EmployeeRequest request);

        Task SetPassportForEmployeeAsync(ImageUploadRequest request);

        Task DeleteByIdAsync(int id);
    }
}
