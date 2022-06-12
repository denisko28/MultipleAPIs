using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HR_BLL.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResponse>> GetAllAsync();

        Task<IEnumerable<EmployeeResponse>> GetAllForManager(int userId);

        Task<EmployeeResponse> GetByIdAsync(int id);

        Task<EmployeeResponse> GetByIdForManager(int id, int userId);

        Task<IEnumerable<EmployeeResponse>> GetByStatusAsync(string status);

        Task<FileResult> GetPassportForEmployeeAsync(int employeeId, UserClaimsModel userClaims);

        Task<int> InsertAsync(EmployeeRequest request);

        Task<bool> UpdateAsync(EmployeeRequest request, UserClaimsModel userClaims);

        Task SetPassportForEmployeeAsync(ImageUploadRequest request, UserClaimsModel userClaims);

        Task DeleteByIdAsync(int id);
    }
}
