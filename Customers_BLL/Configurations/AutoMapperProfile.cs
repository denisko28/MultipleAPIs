using AutoMapper;
using Common.Events.AppointmentEvents;
using Common.Events.BranchEvents;
using Common.Events.ServiceEvents;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_DAL.Entities;
using Google.Protobuf.WellKnownTypes;
using PossibleTime = Customers_DAL.Entities.PossibleTime;

namespace Customers_BLL.Configurations
{
    public class AutoMapperProfile : Profile
    {
        private void CreateAppointmentMaps()
        {
            CreateMap<AppointmentRequest, Appointment>();

            CreateMap<AppointmentPostRequest, Appointment>();

            CreateMap<Appointment, AppointmentResponse>();

            CreateMap<Appointment, CustomersAppointmentResponse>()
                .ForMember(response => response.BranchAddress,
                    options =>
                        options.MapFrom(appointment => appointment.Branch.Address));

            CreateMap<Appointment, Protos.AppointmentResponse>()
                .ForMember(response => response.AppDate,
                    options =>
                        options.MapFrom(appointment => appointment.AppDate.ToTimestamp()))
                .ForMember(response => response.BeginTime,
                    options =>
                        options.MapFrom(appointment => appointment.BeginTime.ToDuration()))
                .ForMember(response => response.EndTime,
                    options =>
                        options.MapFrom(appointment => appointment.EndTime.ToDuration()));;
            
            CreateMap<Appointment, Protos.CustomersAppointmentResponse>()
                .ForMember(response => response.BranchAddress,
                    options =>
                        options.MapFrom(appointment => appointment.Branch.Address))
                .ForMember(response => response.AppDate,
                    options =>
                        options.MapFrom(appointment => appointment.AppDate.ToTimestamp()))
                .ForMember(response => response.BeginTime,
                    options =>
                        options.MapFrom(appointment => appointment.BeginTime.ToDuration()))
                .ForMember(response => response.EndTime,
                    options =>
                        options.MapFrom(appointment => appointment.EndTime.ToDuration()));

            CreateMap<FinishedAppointmentEvent, Appointment>();
        }

        private void CreateCustomerMaps()
        {
            CreateMap<CustomerRequest, Customer>();

            CreateMap<CustomerRegisterRequest, Customer>();

            CreateMap<Customer, CustomerResponse>();

            CreateMap<Customer, Protos.CustomerResponse>();
        }

        private void CreateEmployeeMaps()
        {
            CreateMap<EmployeeRegisterRequest, Employee>();
        }

        private void CreateBarberMaps()
        {
            CreateMap<BarberRegisterRequest, Barber>();
        }

        private void CreateServiceMaps()
        {
            CreateMap<Service, ServiceResponse>();

            CreateMap<ServiceInsertedEvent, Service>();

            CreateMap<ServiceUpdatedEvent, Service>();
        }

        private void CreateBranchMaps()
        {
            CreateMap<BranchInsertedEvent, Branch>();
            CreateMap<BranchUpdatedEvent, Branch>();
        }

        private void CreatePossibleTimeMap()
        {
            CreateMap<PossibleTime, Protos.PossibleTimeResponse>()
                .ForMember(response => response.Time, 
                    options => options
                        .MapFrom(entity => entity.Time.ToDuration()));
        }

        public AutoMapperProfile()
        {
            CreateAppointmentMaps();
            CreateCustomerMaps();
            CreateEmployeeMaps();
            CreateBarberMaps();
            CreateServiceMaps();
            CreateBranchMaps();
            CreatePossibleTimeMap();
        }
    }
}