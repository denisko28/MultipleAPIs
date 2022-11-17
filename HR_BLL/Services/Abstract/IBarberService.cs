using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using IdentityServer.Helpers;

namespace HR_BLL.Services.Abstract
{
    public interface IBarberService
    {
        Task<IEnumerable<BarberResponse>> GetAllAsync();

        Task<BarberResponse> GetByIdAsync(int id);

        Task<IEnumerable<BarbersAppointmentResponse>> GetBarbersAppointmentsAsync(int barberId, string dateStr, UserClaimsModel userClaims);

        Task<IEnumerable<BarberResponse>> GetByBranchIdAsync(int branchId);
        
        Task<int> InsertAsync(BarberRequest request);

        Task<bool> UpdateAsync(BarberRequest request, UserClaimsModel userClaims);

        Task DeleteByIdAsync(int id);
    }
}
