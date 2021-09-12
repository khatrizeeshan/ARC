using System;

namespace ARC.Domain
{
    public interface IRequestDocument
    {
        bool HasSent { get; set; }
        string ContactName { get; set; }
        string ContactEmail { get; set; }
        DateTime? SubmittedOn { get; set; }
        DateTime? RepliedOn { get; set; }
        DateTime? ReceivedOn { get; set; }
        string Response { get; set; }
        string DocumentId { get; set; }
        string DocumentLink { get; set; }
        string DocumentStatus { get; set; }
    }
}
