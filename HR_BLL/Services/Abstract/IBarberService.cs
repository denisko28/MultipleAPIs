using System.Collections.Generic;
using System.Threading.Tasks;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using IdentityServer.Helpers;

namespace HR_BLL.Services.Abstract
{
    public interface IBarberService
    {
        Task<IEnumerable<BarberResponseDto>> GetAllAsync();

        Task<BarberResponseDto> GetByIdAsync(int id);

        Task<IEnumerable<BarbersAppointmentResponseDto>> GetBarbersAppointmentsAsync(int barberId, string dateStr, UserClaimsModel userClaims);

        Task<IEnumerable<BarberResponseDto>> GetByBranchIdAsync(int branchId);
        
        Task<int> InsertAsync(BarberRequestDto requestDto);

        Task<bool> UpdateAsync(BarberRequestDto requestDto, UserClaimsModel userClaims);

        Task DeleteByIdAsync(int id);
    }
}
