namespace ARC.Domain
{
    using System;
    using System.Collections.Generic;

    public class AuthorizationRequest : Entity, IEventLog, IRequestDocument
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int EngagementId { get; set; }
        public Engagement Engagement { get; set; }

        public string RequestorName { get; set; }

        public int MaximumReminders { get; set; }
        public int RemindersCount { get; set; }

        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public DateTime SubmittedOn { get; set; }
        public DateTime? RepliedOn { get; set; }
        public DateTime? ReceivedOn { get; set; }
        public string Response { get; set; }
        public string DocumentId { get; set; }
        public string DocumentLink { get; set; }
        public string DocumentStatus { get; set; }

        public ICollection<EventLog> EventLogs { get; set; }

    }
}

