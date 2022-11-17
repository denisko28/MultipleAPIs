using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Exceptions;
using Customers_BLL.Services.Abstract;
using IdentityServer.Helpers;

namespace Customers_BLL.Services.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly ICustomerRepository customerRepository;
        
        private readonly IAppointmentRepository appointmentRepository;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            customerRepository = unitOfWork.CustomerRepository;
            appointmentRepository = unitOfWork.AppointmentRepository;
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllAsync()
        {
            var customers = await customerRepository.GetAllAsync();
            throw new NotImplementedException("Test with gRPC aggregator");
            return customers.Select(mapper.Map<Customer, CustomerResponse>);
        }
        
        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            throw new NotImplementedException("Test with gRPC aggregator");
            return mapper.Map<Customer, CustomerResponse>(customer);
        }
        
        public async Task<IEnumerable<CustomersAppointmentResponse>> GetCustomersAppointments(int customerId, UserClaimsModel userClaims)
        {
            if (userClaims.Role != UserRoles.Admin && userClaims.UserId != customerId)
                throw new ForbiddenAccessException($"You don't have access to appointments of the customer with id: {customerId}");
                
            throw new NotImplementedException("Test with gRPC aggregator");
            var appointments = await customerRepository.GetCustomersAppointments(customerId);
            return appointments.Select(mapper.Map<Appointment, CustomersAppointmentResponse>);;
        }

        public async Task InsertAsync(CustomerRequest request)
        {
            var entity = mapper.Map<CustomerRequest, Customer>(request);
            await customerRepository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomerRequest request, UserClaimsModel userClaims)
        {
            if (userClaims.Role != UserRoles.Admin && userClaims.UserId != request.UserId)
                throw new ForbiddenAccessException($"You don't have access to edit customer with id: {request.UserId}");
            
            var entity = mapper.Map<CustomerRequest, Customer>(request);
            await customerRepository.UpdateAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await customerRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
