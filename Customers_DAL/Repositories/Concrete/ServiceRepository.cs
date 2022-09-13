using System.Threading.Tasks;
using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Customers_DAL.Repositories.Concrete
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly BarbershopDbContext dbContext;

        private readonly DbSet<Service> table;

        public ServiceRepository(BarbershopDbContext dBContext)
        {
            dbContext = dBContext;
            table = dbContext.Set<Service>();
        }

        public async Task<Service> GetById(int id)
        {
            var service = await table
                .SingleOrDefaultAsync(service => service.Id == id);

            return service ?? throw new EntityNotFoundException(nameof(Service), id);
        }
    }
}