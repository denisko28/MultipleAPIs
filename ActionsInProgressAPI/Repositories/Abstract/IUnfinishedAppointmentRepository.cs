using ActionsInProgressAPI.Entities;

namespace ActionsInProgressAPI.Repositories.Abstract
{
    public interface IUnfinishedAppointmentRepository
    {
        Task<UnfinishedAppointment?> GetAsync(int customersUserId);
        
        Task<UnfinishedAppointment> InsertAsync(UnfinishedAppointment unfinishedAppointment);
        
        Task UpdateAsync(UnfinishedAppointment unfinishedAppointment);
        
        Task DeleteByIdAsync(int customersUserId);
    }
}
