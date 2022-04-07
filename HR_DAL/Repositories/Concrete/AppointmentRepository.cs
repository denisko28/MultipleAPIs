using Dapper;
using MultipleAPIs.HR_DAL.Connection.Abstract;
using MultipleAPIs.HR_DAL.Entities;
using MultipleAPIs.HR_DAL.Repositories.Abstract;

namespace MultipleAPIs.HR_DAL.Repositories.Concrete
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByBarberIdAndDate(int barberId, string date) 
        {
            var sql = "SELECT * FROM Appointment WHERE BarberId = @BarberId AND AppDate >= CONVERT(date, @_Date)";
            var values = new { BarberId = barberId, _Date = date };
            return await connection.Connect.QueryAsync<Appointment>(sql, values);
        }
    }
}
