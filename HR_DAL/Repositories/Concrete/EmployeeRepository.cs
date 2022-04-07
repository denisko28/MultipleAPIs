using Dapper;
using MultipleAPIs.HR_DAL.Connection.Abstract;
using MultipleAPIs.HR_DAL.Entities;
using MultipleAPIs.HR_DAL.Repositories.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultipleAPIs.HR_DAL.Repositories.Concrete
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConnectionFactory connectionFactory):base(connectionFactory)
        {
        }

        public async Task<IEnumerable<Employee>> GetByStatusIdAsync(int StatusId)
        {
            string sql = @"SELECT * FROM Employee WHERE EmployeeStatusId = @StatusId";
            IEnumerable<Employee> results = await connection.Connect.QueryAsync<Employee>(sql, new { StatusId = StatusId });
            return results;
        }
    }
}
