using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using IdentityServer.Helpers;

namespace Customers_BLL.Services.Abstract
{
    public interface IAppointmentService
    {
      Task<IEnumerable<AppointmentResponse>> GetAllAsync(UserClaimsModel userClaims);

      Task<AppointmentResponse> GetByIdAsync(int id, UserClaimsModel userClaims);

      Task<IEnumerable<AppointmentResponse>> GetByDateAsync(string dateStr);

      Task<IEnumerable<ServiceResponse>> GetAppointmentServicesAsync(int appointmentId);

      Task<IEnumerable<TimeResponse>> GetAvailableTimeAsync(int barberId, int duration, string dateStr);

      Task UpdateAsync(AppointmentRequest request, UserClaimsModel userClaims);

      Task DeleteByIdAsync(int id);
    }
}
