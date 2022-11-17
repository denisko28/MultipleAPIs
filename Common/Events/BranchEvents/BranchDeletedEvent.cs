namespace Common.Events.BranchEvents
{
    public class BranchDeletedEvent : BaseEvent
    {
        public int Id { get; set; }
    }
}