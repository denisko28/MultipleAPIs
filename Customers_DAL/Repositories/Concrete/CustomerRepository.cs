using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Customers_DAL.Repositories.Concrete
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BarbershopDbContext dBContext) : base(dBContext)
        {
        }
        
        public override async Task<Customer> GetByIdAsync(int id)
        {
            var appointment = await Table
                .Include(customer => customer.User)
                .SingleOrDefaultAsync(customer => customer.UserId == id);
            
            return appointment ?? throw new EntityNotFoundException(nameof(Customer), id);
        }
        
        public override async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await Table
                .Include(customer => customer.User)
                .ToListAsync();
        }

        public override async Task<Customer> GetCompleteEntityAsync(int id)
        {
            var customer = await Table.Include(customer => customer.User)
                                     .Include(customer => customer.Appointments)
                                     .SingleOrDefaultAsync(customer => customer.UserId == id);

            return customer ?? throw new EntityNotFoundException(nameof(Customer), id);
        }
        
        public async Task<IEnumerable<Appointment>> GetCustomersAppointments(int id)
        {
            var customerWithAppointments = await Table
                .Include(customer => customer.Appointments)
                    .ThenInclude(appointment => appointment.Barber)
                        .ThenInclude(barber => barber!.Employee)
                            .ThenInclude(employee => employee.User)
                .Include(customer => customer.Appointments)
                    .ThenInclude(appointment => appointment.AppointmentServices)
                .SingleOrDefaultAsync(customer => customer.UserId == id);

            if (customerWithAppointments == null)
                throw new EntityNotFoundException(nameof(Customer), id);
                
            return customerWithAppointments.Appointments;
        }
    }
}
