using AutoMapper;
using Services_Application.Commands;
using Services_Application.DTO.Requests;
using Services_Application.DTO.Responses;
using Services_Domain.Entities;

namespace Services_Application.Configurations
{
    public class AutoMapperProfile : Profile
    {
        // private void CreateAppointmentMaps()
        // {
        //     CreateMap<AppointmentRequest, Appointment>()
        //         .ForMember(
        //             response => response.AppointmentStatusId,
        //             options =>
        //                 options.MapFrom(appResponse => AppointmentStatusHelper.GetIntStatus(appResponse.AppointmentStatus!))
        //         );
        //     
        //     CreateMap<Appointment, AppointmentResponse>()
        //         .ForMember(
        //             response => response.AppointmentStatus,
        //             options => 
        //                 options.MapFrom(appointment => AppointmentStatusHelper.GetStringStatus(appointment.AppointmentStatusId))
        //         );
        // }

        private void CreateServiceMaps()
        {
            CreateMap<ServiceRequest, Service>();
            
            CreateMap<Service, ServiceResponse>();
        }
        
        private void CreateServiceDiscountMaps()
        {
            CreateMap<ServiceDiscountRequest, ServiceDiscount>();
            
            CreateMap<ServiceDiscount, ServiceDiscountResponse>();
        }

        public AutoMapperProfile()
        {
            //CreateAppointmentMaps();
            CreateServiceMaps();
            CreateServiceDiscountMaps();
        }
    }
}
