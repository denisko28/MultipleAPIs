using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;

namespace HR_BLL.Services.Abstract
{
    public interface IDayOffService
    {
        Task<IEnumerable<DayOffResponse>> GetAllAsync();

        Task<DayOffResponse> GetByIdAsync(int id);

        Task<int> InsertAsync(DayOffRequest request);

        Task<bool> UpdateAsync(DayOffRequest request);

        Task DeleteByIdAsync(int id);
    }
}
