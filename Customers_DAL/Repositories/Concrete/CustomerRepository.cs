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

        public override async Task<Customer> GetCompleteEntityAsync(int id)
        {
            var customer = await Table.Include(customer => customer.User)
                                     .Include(customer => customer.Appointments)
                                     .SingleOrDefaultAsync(customer => customer.Id == id);

            return customer ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
        }
    }
}
