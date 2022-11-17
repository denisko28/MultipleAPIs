using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Customers_DAL.Repositories.Concrete
{
    public class AppointmentServiceRepository : IAppointmentServiceRepository
    {
        private readonly BarbershopDbContext dbContext;

        private readonly DbSet<AppointmentService> table;
        
        public AppointmentServiceRepository(BarbershopDbContext dbContext)
        {
            this.dbContext = dbContext;
            table = this.dbContext.Set<AppointmentService>();
        }
        
        public async Task InsertRangeAsync(IEnumerable<AppointmentService> entities)
        {
            await table.AddRangeAsync(entities);
        }
    }
}