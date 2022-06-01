using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Customers_DAL.Repositories.Concrete
{
    public class BarberRepository : IBarberRepository
    {
        private readonly BarbershopDbContext dbContext;

        private readonly DbSet<Barber> table;

        public BarberRepository(BarbershopDbContext dBContext)
        {
            dbContext = dBContext;
            table = dbContext.Set<Barber>();
        }

        public async Task<IEnumerable<DayOff>> GetBarbersDayOffs(int barberId)
        {
            var barber = await table
                .Include(barber => barber.Employee)
                    .ThenInclude(employee => employee.EmployeeDayOffs)
                        .ThenInclude(employeeDayOffs => employeeDayOffs.DayOff)
                .SingleOrDefaultAsync(barber => barber.EmployeeUserId == barberId);

            if (barber == null)
                throw new EntityNotFoundException(nameof(Barber), barberId);

            var dayOffs = barber.Employee.EmployeeDayOffs
                .Select(employeeDayOffs => employeeDayOffs.DayOff);
            
            return dayOffs!;
        }
    }
}