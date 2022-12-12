using ActionsInProgressAPI.Entities;
using AutoMapper;
using Common.Events.AppointmentEvents;

namespace ActionsInProgressAPI.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateAppointmentMaps();
        }

        private void CreateAppointmentMaps()
        {
            CreateMap<UnfinishedAppointment, FinishedAppointmentEvent>();
        }
    }
}