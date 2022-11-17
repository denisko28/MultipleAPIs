using GrpcAggregator.DTO.Response;

namespace GrpcAggregator.Services.Abstract;

public interface ICustomersService
{
    Task<IEnumerable<CustomerResponse>> GetAllAsync();

    Task<CustomerResponse> GetByIdAsync(int id);

    Task<IEnumerable<CustomersAppointmentResponse>> GetCustomersAppointmentsAsync(int customerId);
}