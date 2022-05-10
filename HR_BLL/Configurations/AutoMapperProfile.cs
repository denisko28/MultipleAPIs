using AutoMapper;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Helpers;
using HR_DAL.Entities;

namespace HR_BLL.Configurations
{
    public class AutoMapperProfile : Profile
    {
        private void CreateBarberMaps()
        {
            CreateMap<BarberRequest, Barber>();
            CreateMap<Barber, BarberResponse>();

            CreateMap<Appointment, BarbersAppointmentsResponse>()
                .ForMember(
                    response => response.AppointmentId,
                    options => options.MapFrom(appointment => appointment.Id)
                )
                .ForMember(
                    response => response.AppointmentStatus,
                    options => options.MapFrom(appointment => AppointmentStatusHelper.GetStringStatus(appointment.AppointmentStatusId))
                );
        }

        private void CreateBranchMaps()
        {
            CreateMap<BranchRequest, Branch>();
            CreateMap<Branch, BranchResponse>();
        }

        private void CreateDayOffMaps()
        {
            CreateMap<DayOffRequest, DayOff>();
            CreateMap<DayOff, DayOffResponse>();
        }

        private void CreateEmployeeMaps()
        {
            CreateMap<EmployeeRequest, Employee>()
                .ForMember(
                    response => response.EmployeeStatusId,
                    options => 
                        options.MapFrom(employeeRequest => EmployeeStatusHelper.GetIntStatus(employeeRequest.EmployeeStatus!))
                );
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(
                    response => response.EmployeeStatus,
                    options => 
                        options.MapFrom(employee => EmployeeStatusHelper.GetStringStatus(employee.EmployeeStatusId))
                );
        }

        private void CreateEmployeeDayOffMaps()
        {
            CreateMap<EmployeeDayOffRequest, EmployeeDayOff>();
            CreateMap<EmployeeDayOff, EmployeeResponse>();
        }

        private void CreateEmployeeStatusMaps()
        {
            CreateMap<EmployeeStatusRequest, EmployeeStatus>();
            CreateMap<EmployeeStatus, EmployeeStatusResponse>();
        }

        public AutoMapperProfile()
        {
            CreateBarberMaps();
            CreateBranchMaps();
            CreateDayOffMaps();
            CreateEmployeeMaps();
            CreateEmployeeDayOffMaps();
            CreateEmployeeStatusMaps();
        }
    }
}
