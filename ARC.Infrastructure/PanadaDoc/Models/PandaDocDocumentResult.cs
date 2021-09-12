using System;

namespace ARC.Infrastructure
{
    public class PandaDocDocumentResult
    {
        public string id { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_modified { get; set; }
        public DateTime? expiration_date { get; set; }
        public string uuid { get; set; }

    }
}
