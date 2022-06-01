using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;

namespace HR_BLL.Services.Abstract
{
    public interface IEmployeeDayOffService
    {
        Task<IEnumerable<EmployeeDayOffResponse>> GetAllCompleteEntities();
        
        Task<IEnumerable<EmployeeDayOffResponse>> GetAllAsync();

        Task<EmployeeDayOffResponse> GetByIdAsync(int id);

        Task<IEnumerable<EmployeeDayOffResponse>> GetCompleteEntitiesByEmployee(int employeeUserId);

        Task<IEnumerable<EmployeeDayOffResponse>> GetCompleteEntitiesByDate(DateTime date);

        Task<int> InsertAsync(EmployeeDayOffRequest request);

        Task<bool> UpdateAsync(EmployeeDayOffRequest request);

        Task DeleteByIdAsync(int id);
    }
}
