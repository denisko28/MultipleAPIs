using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Customers_DAL.Entities;

namespace Customers_DAL.Repositories.Abstract
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetByDateAsync(DateTime date);

        Task<IEnumerable<Appointment>> GetByDateAndBarberAsync(DateTime date, int barberId);

        Task<IEnumerable<Service>> GetAppointmentServicesAsync(int id);
    }
}
