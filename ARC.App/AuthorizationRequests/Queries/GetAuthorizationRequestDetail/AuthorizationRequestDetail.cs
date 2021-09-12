using ARC.App.Common.Mappings;
using ARC.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace ARC.App.AuthorizationRequests
{
    public class AuthorizationRequestDetail : IMapFrom<AuthorizationRequest>
    {
        public string Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int EngagementId { get; set; }
        public string EngagementName { get; set; }

        public string RequestorName { get; set; }

        public int MaximumReminders { get; set; }
        public int RemindersCount { get; set; }

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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthorizationRequest, AuthorizationRequestDetail>()
                .ForMember(e => e.EngagementName, e => e.MapFrom(e => e.Engagement.Name))
                .ForMember(e => e.ClientName, e => e.MapFrom(e => e.Engagement.Client.Name));
        }
    }
}
