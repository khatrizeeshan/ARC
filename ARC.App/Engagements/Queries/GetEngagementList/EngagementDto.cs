using ARC.App.Common.Mappings;
using ARC.Domain;
using AutoMapper;

namespace ARC.App.Engagements
{
    public class EngagementDto : IMapFrom<Engagement>
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ManagerName { get; set; }
        public string PartnerName { get; set; }
        public string GroupName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Engagement, EngagementDto>();
        }

    }
}
