using AutoMapper;
using Common.Events.BranchEvents;
using Google.Protobuf.WellKnownTypes;
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

            CreateMap<Barber, Protos.BarberResponse>();
        }

        private void CreateBranchMaps()
        {
            CreateMap<BranchPostRequest, Branch>();
            
            CreateMap<BranchRequest, Branch>();
            
            CreateMap<Branch, BranchResponse>();

            CreateMap<Branch, BranchInsertedEvent>();
            
            CreateMap<Branch, BranchUpdatedEvent>();
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
            
            CreateMap<DayOff, Protos.BarbersDayOffResponse>()
                .ForMember(
                target => target.Date,
                options => 
                    options.MapFrom(request => request.Date_.ToTimestamp())
            );
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
