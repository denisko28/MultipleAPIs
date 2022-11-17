using AutoMapper;
using GrpcAggregator.Protos;
using GrpcAggregator.Services.Abstract;
using CustomerResponse = GrpcAggregator.DTO.Response.CustomerResponse;
using CustomersAppointmentResponse = GrpcAggregator.DTO.Response.CustomersAppointmentResponse;

namespace GrpcAggregator.Services.Concrete;

public class CustomersService : ICustomersService
{
    private readonly Customers.CustomersClient _customersClient;

    private readonly User.UserClient _userClient;

    private readonly IMapper _mapper;

    public CustomersService(Customers.CustomersClient customersClient, User.UserClient userClient, IMapper mapper)
    {
        _customersClient = customersClient;
        _userClient = userClient;
        _mapper = mapper;
    }

    private CustomerResponse ExtendCustomer(Protos.CustomerResponse customer, UserResponse user)
    {
        var extendedCustomer = _mapper.Map<Protos.CustomerResponse, CustomerResponse>(customer);
        extendedCustomer.FirstName = user.FirstName;
        extendedCustomer.LastName = user.LastName;
        extendedCustomer.Avatar = user.Avatar;
        return extendedCustomer;
    }

    public async Task<IEnumerable<CustomerResponse>> GetAllAsync()
    {
        var customersResp = await _customersClient.GetAllAsync(new GetAllCustomersRequest());
        var customers = customersResp.Data;
        
        var usersRequest = new UsersRequest();
        foreach (var customer in customers)
        {
            var userRequest = new UserRequest {UserId = customer.UserId};
            usersRequest.Data.Add(userRequest);
        }
        var usersResp = await _userClient.GetUsersByIdsAsync(usersRequest);
        var users = usersResp.Data;
        
        var response = new List<CustomerResponse>();
        for (var index = 0; index < customers.Count; index++)
        {
            var customer = customers[index];
            var user = users[index];
            response.Add(ExtendCustomer(customer, user));
        }

        return response;
    }

    public async Task<CustomerResponse> GetByIdAsync(int id)
    {
        var customer = await _customersClient.GetByIdAsync(new GetCustomerByIdRequest{Id = id});
        var user = await _userClient.GetUserByIdAsync(new UserRequest {UserId = customer.UserId});
        return ExtendCustomer(customer, user);
    }

    public async Task<IEnumerable<CustomersAppointmentResponse>> GetCustomersAppointmentsAsync(int customerId)
    {
        var appointmentsReq = 
            await _customersClient.GetCustomersAppointmentsAsync(new GetCustomerByIdRequest{Id = customerId});
        var appointments = appointmentsReq.Data;
        
        var usersRequest = new UsersRequest();
        foreach (var appointment in appointments)
        {
            var userRequest = new UserRequest {UserId = appointment.BarberUserId};
            usersRequest.Data.Add(userRequest);
        }
        var usersResp = await _userClient.GetUsersByIdsAsync(usersRequest);
        var users = usersResp.Data;

        var responses = new List<CustomersAppointmentResponse>();
        for (var index = 0; index < appointments.Count; index++)
        {
            var appointment = appointments[index];
            var response = _mapper.Map<Protos.CustomersAppointmentResponse, CustomersAppointmentResponse>(appointment);
            var barberUser = users[index];
            response.Avatar = barberUser.Avatar;
            response.BarberName = barberUser.FirstName + " " + barberUser.LastName;
            responses.Add(response);
        }

        return responses;
    }

    // response.FirstName = user.FirstName;
    // response.LastName = user.LastName;
    // response.Avatar = user.Avatar;
    // return response;

    // var barberIds = appointments.Select(appointment => appointment.BarberUserId);
    // var users = new List<User>(); // Request users with barberIds from IdentityAPI using gRPC
    // var responses = new List<CustomersAppointmentResponse>();
    // for (var index = 0; index < appointments.Count; index++)
    // {
    //     var appointment = appointments[index];
    //     var response = mapper.Map<Appointment, CustomersAppointmentResponse>(appointment);
    //     var barberUser = users[index];
    //     response.Avatar = barberUser.Avatar;
    //     response.BarberName = barberUser.FirstName + barberUser.LastName;
    //     responses.Add(response);
    // }
}