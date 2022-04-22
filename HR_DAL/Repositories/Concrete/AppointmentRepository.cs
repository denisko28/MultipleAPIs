using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using HR_DAL.Connection.Abstract;
using HR_DAL.Entities;
using HR_DAL.Repositories.Abstract;

namespace HR_DAL.Repositories.Concrete
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByBarberIdAndDate(int barberId, string date) 
        {
            const string sql = "SELECT * FROM Appointment WHERE BarberId = @BarberId AND AppDate >= CONVERT(date, @_Date)";
            var values = new { BarberId = barberId, _Date = date };
            return await Connection.Connect.QueryAsync<Appointment>(sql, values);
        }
    }
}
