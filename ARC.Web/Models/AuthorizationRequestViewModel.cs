using ARC.App.AuthorizationRequests;
using ARC.App.Common.Mappings;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ARC.Web.Models
{
    public class AuthorizationRequestViewModel : IMapFrom<AuthorizationRequestDetail>
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public int EngagementId { get; set; }
        public string EngagementName { get; set; }

        public string ContactName { get; set; }
        public string ContactEmail { get; set; }

        public int MaximumReminders { get; set; }
        public int RemindersCount { get; set; }

        public bool HasSent { get; set; }
        public DateTime? SubmittedOn { get; set; }
        public DateTime? RepliedOn { get; set; }
        public DateTime? ReceivedOn { get; set; }
        public string Response { get; set; }
        public string DocumentId { get; set; }
        public string DocumentLink { get; set; }
        public string DocumentStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthorizationRequestDetail, AuthorizationRequestViewModel>();
        }
    }
}
