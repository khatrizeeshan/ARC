using ARC.App.Common.Mappings;
using ARC.Domain;
using AutoMapper;
using System;

namespace ARC.App.AuthorizationRequests
{
    public class AuthorizationRequestDto : IMapFrom<AuthorizationRequest>
    {
        public string Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int EngagementId { get; set; }
        public string EngagementName { get; set; }

        public bool HasSent { get; set; }
        public DateTime? SubmittedOn { get; set; }

        public string ContactName { get; set; }
        public string ContactEmail { get; set; }

        public string DocumentId { get; set; }
        public string DocumentLink { get; set; }
        public string DocumentStatus { get; set; }

        public string RequestorName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthorizationRequest, AuthorizationRequestDto>()
                .ForMember(e => e.EngagementName, e => e.MapFrom(e => e.Engagement.Name))
                .ForMember(e => e.ClientName, e => e.MapFrom(e => e.Engagement.Client.Name));
        }

    }
}
