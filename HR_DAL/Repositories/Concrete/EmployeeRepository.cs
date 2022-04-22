using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using HR_DAL.Connection.Abstract;
using HR_DAL.Entities;
using HR_DAL.Repositories.Abstract;

namespace HR_DAL.Repositories.Concrete
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConnectionFactory connectionFactory):base(connectionFactory)
        {
        }

        public async Task<IEnumerable<Employee>> GetByStatusIdAsync(int statusId)
        {
            const string sql = @"SELECT * FROM Employee WHERE EmployeeStatusId = @StatusId";
            IEnumerable<Employee> results = await Connection.Connect.QueryAsync<Employee>(sql, new { StatusId = statusId });
            return results;
        }
    }
}
