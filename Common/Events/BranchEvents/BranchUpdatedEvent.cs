namespace Common.Events.BranchEvents
{
    public class BranchUpdatedEvent : BaseEvent
    {
        public int Id { get; set; }

        public string Descript { get; set; } = null!;
        
        public string Address { get; set; } = null!;
    }
}