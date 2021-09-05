using ARC.Domain;
using System;

namespace ARC.Domain
{
    public class EventLog : Entity<int>
    {
        public string AssociatedWith { get; set; }

        public int? AssociatedWithId { get; set; }

        public string Username { get; set; }

        public string Event { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
