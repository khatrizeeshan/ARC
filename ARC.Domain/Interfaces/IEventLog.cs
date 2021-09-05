using System.Collections.Generic;

namespace ARC.Domain
{
    public interface IEventLog
    {
        public ICollection<EventLog> EventLogs { get; set; }
    }
}
