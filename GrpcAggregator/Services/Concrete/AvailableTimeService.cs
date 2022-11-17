using Google.Protobuf.WellKnownTypes;
using GrpcAggregator.DTO.Response;
using GrpcAggregator.Protos;
using GrpcAggregator.Services.Abstract;

namespace GrpcAggregator.Services.Concrete;

public class AvailableTimeService : IAvailableTimeService
{
    private readonly PossibleTime.PossibleTimeClient _possibleTimeClient;

    private readonly Appointments.AppointmentsClient _appointmentsClient;

    private readonly Barbers.BarbersClient _barbersClient;

    public AvailableTimeService(PossibleTime.PossibleTimeClient possibleTimeClient,
        Appointments.AppointmentsClient appointmentsClient, Barbers.BarbersClient barbersClient)
    {
        _possibleTimeClient = possibleTimeClient;
        _appointmentsClient = appointmentsClient;
        _barbersClient = barbersClient;
    }

    public async Task<IEnumerable<TimeResponse>> GetAvailableTimeAsync(int barberId, int duration, string dateStr)
    {
        var availableTime = new List<TimeResponse>();
        var barbersDayOffsReq = await _barbersClient.GetBarbersDayOffsAsync(new GetBarberByIdRequest {Id = barberId});
        var barbersDayOffs = barbersDayOffsReq.Data;

        var date = DateTime.SpecifyKind(DateTime.Parse(dateStr), DateTimeKind.Utc);
        if (barbersDayOffs.Any(dayOff => dayOff.Date.ToDateTime().Equals(date)))
            return availableTime;

        var possibleTimeReq =
            await _possibleTimeClient.GetAllAsync(new GetAllPossibleTimeRequest());
        var possibleTime =
            possibleTimeReq.Data.Select(possibleTime => possibleTime.Time.ToTimeSpan()).ToList();

        var barbersAppointsForDateReq =
            await _appointmentsClient.GetByDateAndBarberAsync(new GetByDateAndBarberRequest
                {Date = date.ToTimestamp(), BarberId = barberId});
        var barbersAppointsForDate = barbersAppointsForDateReq.Data.ToList();

        for (var i = 0; i < barbersAppointsForDate.Count;)
        {
            for (var j = 0; j < possibleTime.Count; j++)
            {
                if (possibleTime[j] <= barbersAppointsForDate[i].BeginTime.ToTimeSpan() ||
                    possibleTime[j] >= barbersAppointsForDate[i].EndTime.ToTimeSpan())
                    continue;

                possibleTime.RemoveAt(j);
                j--;
            }

            barbersAppointsForDate.RemoveAt(i);
        }

        for (var i = 0; i < possibleTime.Count; i++)
        {
            var available = true;
            var stepSize = new TimeSpan(0, 15, 0);
            var steps = duration / 15;
            for (var j = 0; (j < possibleTime.Count && j < steps); j++)
            {
                var expectedTime = possibleTime[i + j].Add(stepSize).ToString();
                if (possibleTime.Any(time => time.ToString() == expectedTime))
                    continue;
                available = false;
                break;
            }

            if (!available)
                continue;
            var beginTime = possibleTime[i];
            var endTime = possibleTime[i].Add(new TimeSpan(0, duration, 0));
            availableTime.Add(new TimeResponse() {BeginTime = beginTime, EndTime = endTime});
        }

        return availableTime;
    }
}