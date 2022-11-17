using GrpcAggregator.DTO.Response;

namespace GrpcAggregator.Services.Abstract;

public interface IBarbersService
{
    Task<IEnumerable<BarberResponse>> GetAllAsync();

    Task<BarberResponse> GetByIdAsync(int id);

    Task<IEnumerable<BarbersAppointmentResponse>> GetBarbersAppointmentsAsync(int barberId);
}