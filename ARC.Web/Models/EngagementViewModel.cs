using ARC.App.Engagements;
using ARC.App.Common.Mappings;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using ARC.App.Clients;

namespace ARC.Web.Models
{
    public class EngagementViewModel : IMapFrom<EngagementDetail>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ManagerName { get; set; }
        public string PartnerName { get; set; }
        public string GroupName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FieldWorkEndDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ClientYearEndDate { get; set; }

        public IList<string> TeamEmailAddresses { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EngagementDetail, EngagementViewModel>();
        }
    }
}
