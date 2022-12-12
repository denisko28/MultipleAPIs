using AutoMapper;
using Common.Events.BranchEvents;
using Google.Protobuf.WellKnownTypes;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Protos;
using HR_DAL.Entities;

namespace HR_BLL.Configurations
{
    public class AutoMapperProfile : Profile
    {
        private void CreateBarberMaps()
        {
            CreateMap<BarberRequestDto, Barber>();
            
            CreateMap<Barber, BarberResponseDto>();

            CreateMap<Appointment, BarbersAppointmentResponseDto>();

            CreateMap<Barber, BarberResponse>();
        }

        private void CreateBranchMaps()
        {
            CreateMap<BranchPostRequestDto, Branch>();
            
            CreateMap<BranchRequestDto, Branch>();
            
            CreateMap<Branch, BranchResponseDto>();

            CreateMap<Branch, BranchInsertedEvent>();
            
            CreateMap<Branch, BranchUpdatedEvent>();
        }

        private void CreateDayOffMaps()
        {
            CreateMap<DayOffRequestDto, DayOff>()
                .ForMember(
                    target => target.Date_,
                    options => 
                        options.MapFrom(request => request.Date)
                );
            
            CreateMap<DayOffPostRequestDto, DayOff>()
                .ForMember(
                    target => target.Date_,
                    options => 
                        options.MapFrom(request => request.Date)
                );
            
            CreateMap<DayOff, DayOffResponseDto>();
            
            CreateMap<DayOff, BarbersDayOffResponse>()
                .ForMember(
                target => target.Date,
                options => 
                    options.MapFrom(request => request.Date_.ToTimestamp())
            );
            
            CreateMap<DayOff, EmployeesDayOffResponse>()
                .ForMember(
                    target => target.Date,
                    options => 
                        options.MapFrom(request => request.Date_.ToTimestamp())
                );
        }

        private void CreateEmployeeMaps()
        {
            CreateMap<EmployeeRequestDto, Employee>();
            
            CreateMap<Employee, EmployeeResponseDto>();
            
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(
                    target => target.PassportImgPath,
                    options => 
                        options.MapFrom(request => request.PassportImgPath ?? "" )
                )
                .ForMember(
                    target => target.Birthday,
                    options => 
                        options.MapFrom(request => request.Birthday.ToTimestamp())
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
