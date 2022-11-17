using System;

namespace Common.Events
{
    public abstract class BaseEvent
    {
        protected BaseEvent()
        {
            EventId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        protected BaseEvent(Guid eventId, DateTime createDate)
        {
            EventId = eventId;
            CreationDate = createDate;
        }
 
        protected Guid EventId { get; }
 
        protected DateTime CreationDate { get; }
    }
}