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

      Task<IEnumerable<AppointmentResponse>> GetAllForManager(int userId);

      Task<AppointmentResponse> GetByIdForManager(int appointmentId, int userId);

      Task<IEnumerable<ServiceResponse>> GetAppointmentServicesAsync(int appointmentId);

      Task<IEnumerable<TimeResponse>> GetAvailableTimeAsync(int barberId, int duration, string dateStr);

      Task InsertAsync(AppointmentPostRequest request);

      Task UpdateAsync(AppointmentRequest request);

      Task UpdateForManagerAsync(AppointmentRequest request, int userId);

      Task UpdateForBarberAsync(AppointmentRequest request, int userId);

      Task DeleteByIdAsync(int id);
    }
}
