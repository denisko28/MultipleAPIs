using AutoMapper;
using Common.Events.BranchEvents;
using Common.Events.ServiceEvents;
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
            
            CreateMap<ServicePostRequest, Service>();
            
            CreateMap<Service, ServiceResponse>();
        }
        
        private void CreateServiceDiscountMaps()
        {
            CreateMap<ServiceDiscountRequest, ServiceDiscount>();
            
            CreateMap<ServiceDiscount, ServiceDiscountResponse>();

            CreateMap<Service, ServiceInsertedEvent>();
            
            CreateMap<Service, ServiceUpdatedEvent>();
        }

        private void CreateBranchMaps()
        {
            CreateMap<BranchRequest, Branch>();

            CreateMap<BranchInsertedEvent, BranchRequest>();
            
            CreateMap<BranchUpdatedEvent, BranchRequest>();
        }

        public AutoMapperProfile()
        {
            //CreateAppointmentMaps();
            CreateServiceMaps();
            CreateServiceDiscountMaps();
            CreateBranchMaps();
        }
    }
}
