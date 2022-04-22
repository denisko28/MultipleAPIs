using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;

namespace HR_BLL.Services.Abstract
{
    public interface IBarberService
    {
        Task<IEnumerable<BarberResponse>> GetAllAsync();

        Task<BarberResponse> GetByIdAsync(int id);

        Task<IEnumerable<BarbersAppointmentsResponse>> GetBarbersAppointmentsAsync(BarbersAppointmentsRequest request);

        Task<int> InsertAsync(BarberRequest request);

        Task<bool> UpdateAsync(BarberRequest request);

        Task DeleteByIdAsync(int id);
    }
}
