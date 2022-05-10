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
      
      Task<AppointmentResponse> GetCompleteEntityAsync(int id);

      Task<IEnumerable<AppointmentResponse>> GetAppointments(string dateString, int barberId);

      Task InsertAsync(AppointmentRequest request);

      Task UpdateAsync(AppointmentRequest request);

      Task DeleteByIdAsync(int id);
    }
}
