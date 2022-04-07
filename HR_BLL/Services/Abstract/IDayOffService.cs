using MultipleAPIs.HR_BLL.DTO.Responses;
using MultipleAPIs.HR_BLL.DTO.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultipleAPIs.HR_BLL.Services.Abstract
{
    public interface IDayOffService
    {
        Task<IEnumerable<DayOffResponse>> GetAllAsync();

        Task<DayOffResponse> GetByIdAsync(int Id);

        Task<int> InsertAsync(DayOffRequest request);

        Task<bool> UpdateAsync(DayOffRequest request);

        Task DeleteByIdAsync(int Id);
    }
}
