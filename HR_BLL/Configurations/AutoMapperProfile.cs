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

            CreateMap<Appointment, BarbersAppointmentResponse>()
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
            CreateMap<DayOffRequest, DayOff>()
                .ForMember(
                    target => target.Date_,
                    options => 
                        options.MapFrom(request => request.Date)
                );
            CreateMap<DayOffPostRequest, DayOff>()
                .ForMember(
                    target => target.Date_,
                    options => 
                        options.MapFrom(request => request.Date)
                );
            CreateMap<DayOff, DayOffResponse>();
        }

        private void CreateEmployeeMaps()
        {
            CreateMap<EmployeeRequest, Employee>()
                .ForMember(
                    target => target.EmployeeStatusId,
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

        public AutoMapperProfile()
        {
            CreateBarberMaps();
            CreateBranchMaps();
            CreateDayOffMaps();
            CreateEmployeeMaps();
        }
    }
}
