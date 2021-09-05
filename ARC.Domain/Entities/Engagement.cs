using System;

namespace ARC.Domain
{
    public class Engagement : Entity
    {
        public int ClientId { get; set; }

        public Client Client { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Manager { get; set; }

        public string Partner { get; set; }

        public string Group { get; set; }

        public DateTime FieldWorkEndDate { get; set; }

        public DateTime ClientYearEndDate { get; set; }

        public string TeamEmailAddresses { get; set; }

    }
}
