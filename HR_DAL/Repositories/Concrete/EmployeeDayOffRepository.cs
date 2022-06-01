using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using HR_DAL.Connection.Abstract;
using HR_DAL.Entities;
using HR_DAL.Repositories.Abstract;

namespace HR_DAL.Repositories.Concrete
{
    public class EmployeeDayOffRepository : GenericRepository<EmployeeDayOff>, IEmployeeDayOffRepository
    {
        public EmployeeDayOffRepository(IConnectionFactory connectionFactory):base(connectionFactory)
        {
        }

        public async Task<IEnumerable<object>> GetAllCompleteEntities()
        {
            const string sql = "SELECT * FROM EmployeeDayOff " + 
                               "INNER JOIN User_ ON EmployeeUserId = User_.Id " + 
                               "INNER JOIN DayOff ON DayOffId = DayOff.Id";
            
            IEnumerable<object> results = await Connection.QueryAsync<object>(sql);
            return results;
        }
        
        public async Task<object> GetCompleteEntityByDayOff(int dayOffId)
        {
            const string sql = "SELECT * FROM EmployeeDayOff " + 
                               "INNER JOIN User_ ON EmployeeUserId = User_.Id " + 
                               "INNER JOIN DayOff ON DayOffId = DayOff.Id " +
                               "WHERE DayOffId = @DayOffId";
            
            var values = new { DayOffId = dayOffId };
            object results = await Connection.QuerySingleAsync<object>(sql, values);
            return results;
        }
        
        public async Task<IEnumerable<DayOff>> GetDayOffsByEmployee(int employeeUserId)
        {
            const string sql = "SELECT * FROM EmployeeDayOff " + 
                               "INNER JOIN DayOff ON DayOffId = DayOff.Id " + 
                               "WHERE EmployeeUserId = @EmployeeId";
            
            var values = new { EmployeeId = employeeUserId };
            var results = await Connection.QueryAsync<DayOff>(sql, values);
            return results;
        }
        
        public async Task<IEnumerable<object>> GetCompleteEntitiesByDate(DateTime date)
        {
            const string sql = "SELECT * FROM EmployeeDayOff " + 
                               "INNER JOIN User_ ON EmployeeUserId = User_.Id " + 
                               "INNER JOIN DayOff ON DayOffId = DayOff.Id " + 
                               "WHERE Date_ = CONVERT(date, @_Date)";
            
            var values = new { _Date = date };
            IEnumerable<object> results = await Connection.QueryAsync<object>(sql, values);
            return results;
        }
    }
}
