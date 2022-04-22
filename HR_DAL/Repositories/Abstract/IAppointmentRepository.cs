using System.Collections.Generic;
using System.Threading.Tasks;
using HR_DAL.Entities;

namespace HR_DAL.Repositories.Abstract
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByBarberIdAndDate(int barberId, string date);
    }
}
