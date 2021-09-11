using System;

namespace ARC.Domain
{
    public class Engagement : Entity<int>
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public string PartnerName { get; set; }
        public string GroupName { get; set; }
        public DateTime? FieldWorkEndDate { get; set; }
        public DateTime? ClientYearEndDate { get; set; }
        public string TeamEmailAddresses { get; set; }
    }
}
