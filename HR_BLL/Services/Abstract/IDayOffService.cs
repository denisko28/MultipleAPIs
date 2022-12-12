using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using IdentityServer.Helpers;

namespace HR_BLL.Services.Abstract
{
    public interface IDayOffService
    {
        Task<IEnumerable<DayOffResponseDto>> GetAllAsync();

        Task<IEnumerable<DayOffResponseDto>> GetAllForManager(int userId);

        Task<DayOffResponseDto> GetByIdAsync(int id);

        Task<DayOffResponseDto> GetByIdForManager(int id, int userId);

        Task<IEnumerable<DayOffResponseDto>> GetDayOffsByEmployee(int employeeUserId, UserClaimsModel userClaims);

        Task<IEnumerable<DayOffResponseDto>> GetDayOffsByEmployeeForManager(int employeeUserId, int userId);

        Task<IEnumerable<DayOffResponseDto>> GetCompleteEntitiesByDate(DateTime date);

        Task<int> InsertAsync(DayOffPostRequestDto requestDto);

        Task<int> InsertForManagerAsync(DayOffPostRequestDto requestDto, int userId);

        Task<bool> UpdateAsync(DayOffRequestDto requestDto);

        Task<bool> UpdateForManager(DayOffRequestDto requestDto, int userId);

        Task DeleteByIdAsync(int id);

        Task DeleteByIdForManager(int id, int userId);
    }
}
