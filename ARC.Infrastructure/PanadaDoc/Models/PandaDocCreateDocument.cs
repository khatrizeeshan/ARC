using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ARC.Infrastructure
{

    public class PandaDocCreateDocument
    {
        public string name { get; set; }

        public string template_uuid { get; set; }

        public string folder_uuid { get; set; }

        public IList<PandaDocRecipient> recipients { get; set; }

        public IList<PandaDocToken> tokens { get; set; }
    }
}
