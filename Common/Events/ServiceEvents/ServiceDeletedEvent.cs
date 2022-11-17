namespace Common.Events.ServiceEvents
{
    public class ServiceDeletedEvent : BaseEvent
    {
        public int Id { get; set; }
    }
}