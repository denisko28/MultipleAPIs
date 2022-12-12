namespace Common.Events.AppointmentEvents
{
    public class FinishedAppointmentEvent : BaseEvent
    {
        public int BarberUserId { get; set; }
        
        public int CustomerUserId { get; set; }
    
        public DateTime AppDate { get; set; }
    
        public TimeSpan BeginTime { get; set; }
    
        public TimeSpan EndTime { get; set; }

        public int[] ServiceIds { get; set; } = Array.Empty<int>();
    }
}