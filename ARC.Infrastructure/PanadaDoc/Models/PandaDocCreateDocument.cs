using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ARC.Infrastructure
{

    public class PandaDocCreateDocument
    {
        public PandaDocCreateDocument()
        {
            recipients = new List<PandaDocRecipient>();
            tokens = new List<PandaDocToken>();
        }

        public string name { get; set; }

        public string template_uuid { get; set; }

        //public string folder_uuid { get; set; }

        public IList<PandaDocRecipient> recipients { get; set; }

        public IList<PandaDocToken> tokens { get; set; }

        public void AddRecipient(string email, string name)
        {
            recipients.Add(new PandaDocRecipient() { email = email, first_name = name, last_name = name });
        }

        public void AddToken(string name, string value)
        {
            tokens.Add(new PandaDocToken() { name = name, value = value });
        }
    }

    public class PandaDocSendDocument
    {
        public string message { get; set; }
        public string subject { get; set; }
        public bool silent { get; set; }
    }
}
