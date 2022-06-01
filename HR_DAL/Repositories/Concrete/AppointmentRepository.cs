using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using HR_DAL.Connection.Abstract;
using HR_DAL.Entities;
using HR_DAL.Repositories.Abstract;

namespace HR_DAL.Repositories.Concrete
{
    public class AppointmentRepository : IAppointmentRepository
    {
        protected readonly IDbConnection Connection;

        public AppointmentRepository(IConnectionFactory connectionFactory)
        {
            Connection = connectionFactory.GetConnection();
            Connection.Open();
        }

        public async Task<IEnumerable<Appointment>> GetAppointments(int barberId, string date) 
        {
            const string sql = "SELECT * FROM Appointment WHERE BarberUserId = @BarberUserId AND AppDate = CONVERT(date, @_Date)";
            var values = new { BarberUserId = barberId, _Date = date };
            return await Connection.QueryAsync<Appointment>(sql, values);
        }
        
        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();
        }
    }
}
