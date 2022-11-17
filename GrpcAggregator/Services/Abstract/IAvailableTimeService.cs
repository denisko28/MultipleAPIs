using GrpcAggregator.DTO.Response;

namespace GrpcAggregator.Services.Abstract;

public interface IAvailableTimeService
{
    Task<IEnumerable<TimeResponse>> GetAvailableTimeAsync(int barberId, int duration, string dateStr);
}