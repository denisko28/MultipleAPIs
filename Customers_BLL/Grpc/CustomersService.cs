using AutoMapper;
using Customers_BLL.Protos;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using Grpc.Core;

namespace Customers_BLL.Grpc;

public class CustomersService : Customers.CustomersBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomersService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _customerRepository = unitOfWork.CustomerRepository;
        _mapper = mapper;
    }

    public override async Task<CustomersResponse> GetAll(GetAllCustomersRequest request, ServerCallContext context)
    {
        var customersResponse = new CustomersResponse();
        var customers = await _customerRepository.GetAllAsync();
        var data = 
            customers.Select(_mapper.Map<Customers_DAL.Entities.Customer, CustomerResponse>);
        customersResponse.Data.AddRange(data);
        return customersResponse;
    }

    public override async Task<CustomerResponse> GetById(GetCustomerByIdRequest request, ServerCallContext context)
    {
        try
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            return _mapper.Map<Customers_DAL.Entities.Customer, CustomerResponse>(customer);
        }
        catch (EntityNotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<CustomersAppointmentsResponse> GetCustomersAppointments(GetCustomerByIdRequest request,
        ServerCallContext context)
    {
        var appointmentsResponse = new CustomersAppointmentsResponse();
        try
        {
            var appointments = await _customerRepository.GetCustomersAppointments(request.Id);
            var data =
                appointments.Select(_mapper.Map<Customers_DAL.Entities.Appointment, CustomersAppointmentResponse>);
            appointmentsResponse.Data.AddRange(data);
            return appointmentsResponse;
        }
        catch (EntityNotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }
}