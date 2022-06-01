using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;

namespace Customers_BLL.Services.Abstract
{
    public interface ICustomerService
    {
      Task<IEnumerable<CustomerResponse>> GetAllAsync();

      Task<CustomerResponse> GetByIdAsync(int id);
      
      Task<CustomerResponse> GetCompleteEntityAsync(int id);

      Task<IEnumerable<CustomersAppointmentResponse>> GetCustomersAppointments(int id);

      Task InsertAsync(CustomerRequest request);

      Task UpdateAsync(CustomerRequest request);

      Task DeleteByIdAsync(int id);
    }
}
