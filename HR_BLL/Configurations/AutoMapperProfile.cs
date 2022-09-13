using AutoMapper;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_DAL.Entities;

namespace HR_BLL.Configurations
{
    public class AutoMapperProfile : Profile
    {
        private void CreateBarberMaps()
        {
            CreateMap<BarberRequest, Barber>();
            
            CreateMap<Barber, BarberResponse>();

            CreateMap<Appointment, BarbersAppointmentResponse>();
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
            CreateMap<EmployeeRequest, Employee>();
            
            CreateMap<Employee, EmployeeResponse>();
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
