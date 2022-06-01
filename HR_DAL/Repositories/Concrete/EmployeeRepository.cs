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
        
        public override async Task<Employee> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM Employee WHERE UserId=@Id";
            var values = new { Id = id };
            return await Connection.QuerySingleAsync<Employee>(sql, values);
        }

        public async Task<IEnumerable<Employee>> GetByStatusIdAsync(int statusId)
        {
            const string sql = @"SELECT * FROM Employee WHERE EmployeeStatusId = @StatusId";
            IEnumerable<Employee> results = await Connection.QueryAsync<Employee>(sql, new { StatusId = statusId });
            return results;
        }
    }
}
