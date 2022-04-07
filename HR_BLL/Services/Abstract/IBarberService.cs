using MultipleAPIs.HR_BLL.DTO.Responses;
using MultipleAPIs.HR_BLL.DTO.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultipleAPIs.HR_BLL.Services.Abstract
{
    public interface IBarberService
    {
        Task<IEnumerable<BarberResponse>> GetAllAsync();

        Task<BarberResponse> GetByIdAsync(int Id);

        Task<IEnumerable<BarbersAppointmentsResponse>> GetBarbersAppointmentsAsync(BarbersAppointmentsRequest request);

        Task<int> InsertAsync(BarberRequest request);

        Task<bool> UpdateAsync(BarberRequest request);

        Task DeleteByIdAsync(int Id);
    }
}
