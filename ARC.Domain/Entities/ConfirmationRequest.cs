namespace ARC.Domain
{
    using System;
    using System.Collections.Generic;

    public class ConfirmationRequest : Entity<int>, IEventLog, IRequestDocument
    {
        public int AuthorizationRequestId { get; set; }
        public AuthorizationRequest AuthorizationRequest { get; set; }

        public string CustomerName { get; set; }
        public string InvoiceId { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal? ChallengedAmount { get; set; }

        public bool HasSent { get; set; }

        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public DateTime? RepliedOn { get; set; }
        public DateTime? ReceivedOn { get; set; }
        public string Response { get; set; }
        public string DocumentId { get; set; }
        public string DocumentLink { get; set; }
        public string DocumentStatus { get; set; }

        public ICollection<EventLog> EventLogs { get; set; }
    }
}

