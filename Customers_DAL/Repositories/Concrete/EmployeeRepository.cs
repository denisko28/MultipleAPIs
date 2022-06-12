using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers_DAL.Entities;
using Customers_DAL.Exceptions;
using Customers_DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Customers_DAL.Repositories.Concrete
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BarbershopDbContext dbContext;

        private readonly DbSet<Employee> table;

        public EmployeeRepository(BarbershopDbContext dBContext)
        {
            dbContext = dBContext;
            table = dbContext.Set<Employee>();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await table.SingleOrDefaultAsync(employee => employee.UserId == id) 
                   ?? throw new EntityNotFoundException(nameof(Employee), id);
        }

        public async Task InsertAsync(Employee employee)
        {
            await table.AddAsync(employee);
        }
    }
}