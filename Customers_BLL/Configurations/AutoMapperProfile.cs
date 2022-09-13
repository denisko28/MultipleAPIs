using AutoMapper;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_DAL.Entities;

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
                .ForMember(
                    response => response.Avatar,
                    options =>
                        options.MapFrom(appointment => appointment.Barber.Employee.User.Avatar)
                )
                .ForMember(
                    response => response.BranchAddress,
                    options =>
                        options.MapFrom(appointment => appointment.Barber.Employee.Branch.Address)
                )
                .ForMember(
                    response => response.BarberName,
                    options =>
                        options.MapFrom(appointment => appointment.Barber.Employee.User.FirstName +
                                                       " " + appointment.Barber.Employee.User.LastName)
                );
        }

        private void CreateCustomerMaps()
        {
            CreateMap<CustomerRequest, Customer>();

            CreateMap<CustomerRegisterRequest, Customer>();

            CreateMap<Customer, CustomerResponse>()
                .ForMember(
                    response => response.FirstName,
                    options => 
                        options.MapFrom(customer => customer.User.FirstName)
                )
                .ForMember(
                    response => response.LastName,
                    options => 
                        options.MapFrom(customer => customer.User.LastName)
                )
                .ForMember(
                    response => response.Avatar,
                    options => 
                        options.MapFrom(customer => customer.User.Avatar)
                );
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
        }

        private void CreateUserMaps()
        {
            CreateMap<User, UserResponse>();
            
            CreateMap<CustomerRegisterRequest, User>()
                .ForMember(
                    user => user.UserName,
                    options => 
                        options.MapFrom(user => user.Email)
                );

            CreateMap<EmployeeRegisterRequest, User>()
                .ForMember(
                    user => user.UserName,
                    options => 
                        options.MapFrom(user => user.Email)
                );

            CreateMap<BarberRegisterRequest, User>()
                .ForMember(
                    user => user.UserName,
                    options => 
                        options.MapFrom(user => user.Email)
                );
        }

        public AutoMapperProfile()
        {
            CreateAppointmentMaps();
            CreateCustomerMaps();
            CreateEmployeeMaps();
            CreateBarberMaps();
            CreateServiceMaps();
            CreateUserMaps();
        }
    }
}
