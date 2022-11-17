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

        public async Task<IEnumerable<Appointment>> GetCustomersAppointments(int id)
        {
            var customerWithAppointments = await Table
                .Include(customer => customer.Appointments)
                    .ThenInclude(appointment => appointment.AppointmentServices)
                .Include(customer => customer.Appointments)
                    .ThenInclude(appointment => appointment.Branch)
                .SingleOrDefaultAsync(customer => customer.UserId == id);

            if (customerWithAppointments == null)
                throw new EntityNotFoundException(nameof(Customer), id);
                
            return customerWithAppointments.Appointments;
        }
    }
}
