using Redis.OM.Modeling;

namespace ActionsInProgressAPI.Entities;

[Document(StorageType = StorageType.Json, Prefixes = new []{"UnfinishedAppointment"})]
public class UnfinishedAppointment
{
    [RedisIdField]
    [Indexed]
    public int CustomerUserId { get; set; }
    
    [Indexed]
    public int StageIndex { get; set; }

    [Indexed]
    public int? BarberUserId { get; set; }
    
    [Indexed]
    public DateTime? AppDate { get; set; }
    
    [Indexed]
    public TimeSpan? BeginTime { get; set; }
    
    [Indexed]
    public TimeSpan? EndTime { get; set; }

    [Indexed] 
    public int[] ServiceIds { get; set; } = Array.Empty<int>();
}