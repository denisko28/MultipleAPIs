using AutoMapper;
using GrpcAggregator.Protos;
using GrpcAggregator.Services.Abstract;
using BarberResponse = GrpcAggregator.DTO.Response.BarberResponse;
using BarbersAppointmentResponse = GrpcAggregator.DTO.Response.BarbersAppointmentResponse;

namespace GrpcAggregator.Services.Concrete;

public class BarbersService : IBarbersService
{
    private readonly Barbers.BarbersClient _barbersClient;

    private readonly Appointments.AppointmentsClient _appointmentsClient;

    private readonly User.UserClient _userClient;

    private readonly IMapper _mapper;

    public BarbersService(Barbers.BarbersClient barbersClient, Appointments.AppointmentsClient appointmentsClient,
        User.UserClient userClient, IMapper mapper)
    {
        _barbersClient = barbersClient;
        _appointmentsClient = appointmentsClient;
        _userClient = userClient;
        _mapper = mapper;
    }

    private BarberResponse ExtendBarber(Protos.BarberResponse barber, UserResponse user)
    {
        var extendedBarber = _mapper.Map<Protos.BarberResponse, BarberResponse>(barber);
        extendedBarber.FirstName = user.FirstName;
        extendedBarber.LastName = user.LastName;
        extendedBarber.Avatar = user.Avatar;
        return extendedBarber;
    }

    public async Task<IEnumerable<BarberResponse>> GetAllAsync()
    {
        var barbersResp = await _barbersClient.GetAllAsync(new GetAllBarbersRequest());
        var barbers = barbersResp.Data;

        var usersRequest = new UsersRequest();
        foreach (var barber in barbers)
        {
            var userRequest = new UserRequest {UserId = barber.EmployeeUserId};
            usersRequest.Data.Add(userRequest);
        }

        var usersResp = await _userClient.GetUsersByIdsAsync(usersRequest);
        var users = usersResp.Data;

        var response = new List<BarberResponse>();
        for (var index = 0; index < barbers.Count; index++)
        {
            var barber = barbers[index];
            var user = users[index];
            response.Add(ExtendBarber(barber, user));
        }

        return response;
    }

    public async Task<BarberResponse> GetByIdAsync(int id)
    {
        var barber = await _barbersClient.GetByIdAsync(new GetBarberByIdRequest {Id = id});
        var user = await _userClient.GetUserByIdAsync(new UserRequest {UserId = barber.EmployeeUserId});
        return ExtendBarber(barber, user);
    }

    public async Task<IEnumerable<BarbersAppointmentResponse>> GetBarbersAppointmentsAsync(int barberId)
    {
        //throw new NotImplementedException("GetBarbersAppointmentsAsync not implemented");
        var appointmentsReq =
            await _appointmentsClient.GetBarbersAppointmentsAsync(new GetBarbersAppointmentsRequest {BarberId = barberId});
        var appointments = appointmentsReq.Data;

        var usersRequest = new UsersRequest();
        foreach (var appointment in appointments)
        {
            var userRequest = new UserRequest {UserId = appointment.BarberUserId};
            usersRequest.Data.Add(userRequest);
        }

        var usersResp = await _userClient.GetUsersByIdsAsync(usersRequest);
        var users = usersResp.Data;

        var responses = new List<BarbersAppointmentResponse>();
        for (var index = 0; index < appointments.Count; index++)
        {
            var appointment = appointments[index];
            var response = _mapper.Map<Protos.AppointmentResponse, BarbersAppointmentResponse>(appointment);
            var customerUser = users[index];
            response.CustomerName = customerUser.FirstName + " " + customerUser.LastName;;
            responses.Add(response);
        }

        return responses;
    }
}