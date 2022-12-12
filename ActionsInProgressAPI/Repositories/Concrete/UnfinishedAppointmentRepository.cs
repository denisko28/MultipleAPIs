using ActionsInProgressAPI.Entities;
using ActionsInProgressAPI.Repositories.Abstract;
using Redis.OM;
using Redis.OM.Searching;

namespace ActionsInProgressAPI.Repositories.Concrete
{
    public class UnfinishedAppointmentRepository : IUnfinishedAppointmentRepository
    {
        private readonly RedisConnectionProvider _provider;
        private readonly RedisCollection<UnfinishedAppointment> _unfinishedAppointments;

        public UnfinishedAppointmentRepository(RedisConnectionProvider provider)
        {
            _provider = provider;
            _unfinishedAppointments = (RedisCollection<UnfinishedAppointment>)
                provider.RedisCollection<UnfinishedAppointment>();
        }
        
        public async Task<UnfinishedAppointment?> GetAsync(int customersUserId)
        {
            return await _unfinishedAppointments.SingleOrDefaultAsync(unfinishedApp =>
                unfinishedApp.CustomerUserId == customersUserId);
        }

        public async Task<UnfinishedAppointment> InsertAsync(UnfinishedAppointment unfinishedAppointment)
        {
            await _unfinishedAppointments.InsertAsync(unfinishedAppointment);
            return unfinishedAppointment;
        }
        
        public async Task UpdateAsync(UnfinishedAppointment unfinishedAppointment)
        {
            await _unfinishedAppointments.UpdateAsync(unfinishedAppointment);
        }

        public async Task DeleteByIdAsync(int customersUserId)
        {
            await _provider.Connection.UnlinkAsync($"UnfinishedAppointment:{customersUserId}");
        }
    }
}
