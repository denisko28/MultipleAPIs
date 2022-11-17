using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using IdentityServer.Helpers;

namespace Customers_BLL.Services.Abstract
{
    public interface ICustomerService
    {
      Task<IEnumerable<CustomerResponse>> GetAllAsync();

      Task<CustomerResponse> GetByIdAsync(int id);

      Task<IEnumerable<CustomersAppointmentResponse>> GetCustomersAppointments(int customerId, UserClaimsModel userClaims);

      Task InsertAsync(CustomerRequest request);

      Task UpdateAsync(CustomerRequest request, UserClaimsModel userClaims);

      Task DeleteByIdAsync(int id);
    }
}
