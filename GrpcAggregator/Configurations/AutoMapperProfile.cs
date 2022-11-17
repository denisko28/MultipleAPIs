using AutoMapper;
using GrpcAggregator.DTO.Response;

namespace GrpcAggregator.Configurations
{
    public class AutoMapperProfile : Profile
    {
        private void CreateCustomerMap()
        {
            CreateMap<Protos.CustomerResponse, CustomerResponse>();
            CreateMap<Protos.CustomersAppointmentResponse, CustomersAppointmentResponse>();
        }

        private void CreateAppointmentMap()
        {
            CreateMap<Protos.AppointmentResponse, BarbersAppointmentResponse>()
                .ForMember(dest => dest.AppDate, 
                    options => options
                    .MapFrom(source => source.AppDate.ToDateTime()))
                .ForMember(dest => dest.BeginTime, 
                    options => options
                        .MapFrom(source => source.BeginTime.ToTimeSpan()))
                .ForMember(dest => dest.EndTime, 
                    options => options
                        .MapFrom(source => source.EndTime.ToTimeSpan()));
        }

        private void CreateBarberMap()
        {
            CreateMap<Protos.BarberResponse, BarberResponse>();
        }

        public AutoMapperProfile()
        {
            CreateCustomerMap();
            CreateAppointmentMap();
            CreateBarberMap();
        }
    }
}