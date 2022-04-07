using MultipleAPIs.HR_DAL.Entities;

namespace MultipleAPIs.HR_DAL.Repositories.Abstract
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByBarberIdAndDate(int barberId, string date);
    }
}
