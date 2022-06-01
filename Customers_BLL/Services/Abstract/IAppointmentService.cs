using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;

namespace Customers_BLL.Services.Abstract
{
    public interface IAppointmentService
    {
      Task<IEnumerable<AppointmentResponse>> GetAllAsync();

      Task<AppointmentResponse> GetByIdAsync(int id);

      Task<IEnumerable<AppointmentResponse>> GetByDateAsync(string dateStr);

      Task<IEnumerable<ServiceResponse>> GetAppointmentServicesAsync(int appointmentId);

      Task<IEnumerable<TimeResponse>> GetAvailableTimeAsync(int barberId, int duration, string dateStr);

      Task InsertAsync(AppointmentPostRequest request);

      Task UpdateAsync(AppointmentRequest request);

      Task DeleteByIdAsync(int id);
    }
}
