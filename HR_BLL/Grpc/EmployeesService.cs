using AutoMapper;
using Grpc.Core;
using HR_BLL.Protos;
using HR_DAL.Entities;
using HR_DAL.Exceptions;
using HR_DAL.Repositories.Abstract;

namespace HR_BLL.Grpc;

public class EmployeesService : Employees.EmployeesBase
{
    private readonly IEmployeeRepository _barberRepository;
    private readonly IEmployeeDayOffRepository _employeeDayOffRepository;
    private readonly IMapper _mapper;

    public EmployeesService(IEmployeeRepository barberRepository, IEmployeeDayOffRepository employeeDayOffRepository,
        IMapper mapper)
    {
        _barberRepository = barberRepository;
        _employeeDayOffRepository = employeeDayOffRepository;
        _mapper = mapper;
    }

    public override async Task<EmployeesResponse> GetAll(GetAllEmployeesRequest request, ServerCallContext context)
    {
        var barbersResponse = new EmployeesResponse();
        var barbers = await _barberRepository.GetAllAsync();
        var data = barbers.Select(_mapper.Map<Employee, EmployeeResponse>);
        barbersResponse.Data.AddRange(data);
        return barbersResponse;
    }

    public override async Task<EmployeeResponse> GetById(GetEmployeeByIdRequest request, ServerCallContext context)
    {
        try
        {
            var barber = await _barberRepository.GetByIdAsync(request.Id);
            return _mapper.Map<Employee, EmployeeResponse>(barber);
        }
        catch (EntityNotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<EmployeesDayOffsResponse> GetEmployeesDayOffs(GetEmployeeByIdRequest request,
        ServerCallContext context)
    {
        var response = new EmployeesDayOffsResponse();
        var dayOffs = await _employeeDayOffRepository.GetDayOffsByEmployee(request.Id);
        var dayOffsData = dayOffs.Select(_mapper.Map<DayOff, EmployeesDayOffResponse>);
        response.Data.AddRange(dayOffsData);

        return response;
    }
}