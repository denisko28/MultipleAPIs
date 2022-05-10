using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_DAL.Entities;

namespace Customers_DAL.Repositories.Abstract
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointments(DateTime date, int barberId);

        Task<IEnumerable<Service>> GetAppointmentServices(int id);
    }
}
