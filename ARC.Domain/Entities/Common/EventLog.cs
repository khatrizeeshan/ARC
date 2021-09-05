using ARC.Domain;
using System;

namespace ARC.Domain
{
    public class EventLog : Entity
    {
        public string Entity { get; set; }

        public int EntityId { get; set; }

        public string Event { get; set; }

        public string Note { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
