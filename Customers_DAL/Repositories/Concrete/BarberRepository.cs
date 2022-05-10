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
            this.dbContext = dBContext;
            table = this.dbContext.Set<Barber>();
        }

        public async Task<IEnumerable<DayOff>> GetBarbersDayOffs(int barberId)
        {
            var barber = await table
                .Include(barber => barber.Employee)
                .ThenInclude(employee => employee!.EmployeeDayOffs)
                .ThenInclude(employeeDayOffs => employeeDayOffs.DayOff)
                .SingleOrDefaultAsync(barber => barber.Id == barberId);

            if (barber == null)
                throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(barberId));

            if (barber.Employee == null)
                return new List<DayOff>();
                
            var dayOffs = barber.Employee.EmployeeDayOffs
                .Select(employeeDayOffs => employeeDayOffs.DayOff);
            
            return dayOffs!;
        }

        private static string GetEntityNotFoundErrorMessage(int id) =>
            $"Barber with id {id} not found.";
    }
}