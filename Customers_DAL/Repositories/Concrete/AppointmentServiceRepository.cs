using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Customers_DAL.Repositories.Concrete
{
    public class AppointmentServiceRepository : GenericRepository<AppointmentService>, IAppointmentServiceRepository
    {
        public AppointmentServiceRepository(BarbershopDbContext dBContext) : base(dBContext)
        {
        }
        
        public override async Task<AppointmentService> GetCompleteEntityAsync(int id)
        {
            var appointmentService = await Table
                .Include(appService => appService.Appointment)
                .Include(appService => appService.Service)
                .SingleOrDefaultAsync(customer => customer.Id == id);

            return appointmentService ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
        }
        
        public async Task InsertRangeAsync(IEnumerable<AppointmentService> entities)
        {
            await Table.AddRangeAsync(entities);
        }
    }
}