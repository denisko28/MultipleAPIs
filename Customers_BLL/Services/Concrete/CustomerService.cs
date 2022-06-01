using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Services.Abstract;

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
            var results = await customerRepository.GetAllAsync();
            return results.Select(mapper.Map<Customer, CustomerResponse>);
        }
        
        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var result = await customerRepository.GetByIdAsync(id);
            return mapper.Map<Customer, CustomerResponse>(result);
        }

        public async Task<CustomerResponse> GetCompleteEntityAsync(int id)
        {
            var result = await customerRepository.GetCompleteEntityAsync(id);
            return mapper.Map<Customer, CustomerResponse>(result);
        }
        
        public async Task<IEnumerable<CustomersAppointmentResponse>> GetCustomersAppointments(int id)
        {
          var result = await customerRepository.GetCustomersAppointments(id);
          return result.Select(mapper.Map<Appointment, CustomersAppointmentResponse>);
        }

        public async Task InsertAsync(CustomerRequest request)
        {
            var entity = mapper.Map<CustomerRequest, Customer>(request);
            await customerRepository.InsertAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(CustomerRequest request)
        {
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
