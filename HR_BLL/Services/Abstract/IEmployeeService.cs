using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using IdentityServer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HR_BLL.Services.Abstract
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResponseDto>> GetAllAsync();

        Task<IEnumerable<EmployeeResponseDto>> GetAllForManager(int userId);

        Task<EmployeeResponseDto> GetByIdAsync(int id);

        Task<EmployeeResponseDto> GetByIdForManager(int id, int userId);

        Task<IEnumerable<EmployeeResponseDto>> GetByStatusAsync(int statusCode);

        Task<FileResult> GetPassportForEmployeeAsync(int employeeId, UserClaimsModel userClaims);

        Task<int> InsertAsync(EmployeeRequestDto requestDto);

        Task<bool> UpdateAsync(EmployeeRequestDto requestDto, UserClaimsModel userClaims);

        Task SetPassportForEmployeeAsync(ImageUploadRequestDto requestDto, UserClaimsModel userClaims);

        Task DeleteByIdAsync(int id);
    }
}
