using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Helpers;

namespace HR_BLL.Services.Abstract
{
    public interface IDayOffService
    {
        Task<IEnumerable<DayOffResponse>> GetAllAsync();

        Task<IEnumerable<DayOffResponse>> GetAllForManager(int userId);

        Task<DayOffResponse> GetByIdAsync(int id);

        Task<DayOffResponse> GetByIdForManager(int id, int userId);

        Task<IEnumerable<DayOffResponse>> GetDayOffsByEmployee(int employeeUserId, UserClaimsModel userClaims);

        Task<IEnumerable<DayOffResponse>> GetDayOffsByEmployeeForManager(int employeeUserId, int userId);

        Task<IEnumerable<DayOffResponse>> GetCompleteEntitiesByDate(DateTime date);

        Task<int> InsertAsync(DayOffPostRequest request);

        Task<int> InsertForManagerAsync(DayOffPostRequest request, int userId);

        Task<bool> UpdateAsync(DayOffRequest request);

        Task<bool> UpdateForManager(DayOffRequest request, int userId);

        Task DeleteByIdAsync(int id);

        Task DeleteByIdForManager(int id, int userId);
    }
}
