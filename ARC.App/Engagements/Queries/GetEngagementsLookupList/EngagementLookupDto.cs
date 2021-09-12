using ARC.App.Common.Mappings;
using ARC.Domain;
using AutoMapper;

namespace ARC.App.Engagements
{
    public class EngagementLookupDto : IMapFrom<Engagement>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Engagement, EngagementLookupDto>();
        }
    }
}
