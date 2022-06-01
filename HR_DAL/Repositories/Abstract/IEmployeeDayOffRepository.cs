using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_DAL.Entities;

namespace HR_DAL.Repositories.Abstract
{
    public interface IEmployeeDayOffRepository : IGenericRepository<EmployeeDayOff>
    {
        Task<IEnumerable<object>> GetAllCompleteEntities();

        Task<object> GetCompleteEntityByDayOff(int dayOffId);

        Task<IEnumerable<DayOff>> GetDayOffsByEmployee(int employeeUserId);

        Task<IEnumerable<object>> GetCompleteEntitiesByDate(DateTime date);
    }
}
