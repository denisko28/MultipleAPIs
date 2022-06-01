using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HR_DAL.Entities;

namespace HR_DAL.Repositories.Abstract
{
    public interface IAppointmentRepository : IDisposable
    {
        Task<IEnumerable<Appointment>> GetAppointments(int barberId, string date);
    }
}
