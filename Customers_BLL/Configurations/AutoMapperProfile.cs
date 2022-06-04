using AutoMapper;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Helpers;
using Customers_DAL.Entities;

namespace Customers_BLL.Configurations
{
    public class AutoMapperProfile : Profile
    {
        // private void CreateBarberMaps()
        // {
        //     CreateMap<BarberRequest, Barber>();
        //     CreateMap<Barber, BarberResponse>();
        //
        //     CreateMap<Appointment, BarbersAppointmentsResponse>()
        //         .ForMember(
        //             response => response.AppointmentId,
        //             options => options.MapFrom(appointment => appointment.Id)
        //         )
        //         .ForMember(
        //             response => response.AppointmentStatus,
        //             options => options.MapFrom(appointment => AppointmentStatusHelper.GetStringStatus(appointment.AppointmentStatusId))
        //         );
        // }

        private void CreateAppointmentMaps()
        {
            CreateMap<AppointmentRequest, Appointment>()
                .ForMember(
                    target => target.AppointmentStatusId,
                    options =>
                        options.MapFrom(appResponse => AppointmentStatusHelper.GetIntStatus(appResponse.AppointmentStatus!))
                );
            
            CreateMap<AppointmentPostRequest, Appointment>()
                .ForMember(
                    target => target.AppointmentStatusId,
                    options =>
                        options.MapFrom(appResponse => AppointmentStatusHelper.GetIntStatus(appResponse.AppointmentStatus!))
                );
            
            CreateMap<Appointment, AppointmentResponse>()
                .ForMember(
                    response => response.AppointmentStatus,
                    options => 
                        options.MapFrom(appointment => AppointmentStatusHelper.GetStringStatus(appointment.AppointmentStatusId))
                );

            CreateMap<Appointment, CustomersAppointmentResponse>()
                .ForMember(
                    response => response.ChairNum,
                    options => 
                        options.MapFrom(appointment => appointment.Barber!.ChairNum)
                )
                .ForMember(
                    response => response.BarberName,
                    options => 
                        options.MapFrom(appointment => appointment.Barber!.Employee!.User!.FirstName +
                                                       " " + appointment.Barber.Employee.User.LastName)
                )
                .ForMember(
                response => response.AppointmentStatus,
                options => 
                    options.MapFrom(appointment => AppointmentStatusHelper.GetStringStatus(appointment.AppointmentStatusId))
                );
        }

        private void CreateCustomerMaps()
        {
            CreateMap<CustomerRequest, Customer>();

            CreateMap<Customer, CustomerResponse>()
                .ForMember(
                    response => response.FirstName,
                    options => 
                        options.MapFrom(customer => customer.User!.FirstName)
                )
                .ForMember(
                    response => response.LastName,
                    options => 
                        options.MapFrom(customer => customer.User!.LastName)
                )
                .ForMember(
                    response => response.Avatar,
                    options => 
                        options.MapFrom(customer => customer.User!.Avatar)
                );
        }
        
        private void CreateServiceMaps()
        {
            CreateMap<Service, ServiceResponse>();
        }

        private void CreateUserMaps()
        {
            CreateMap<User, UserResponse>();
        }

        public AutoMapperProfile()
        {
            CreateAppointmentMaps();
            CreateCustomerMaps();
            CreateServiceMaps();
            CreateUserMaps();
        }
    }
}
