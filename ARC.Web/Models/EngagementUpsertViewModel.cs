using ARC.App.Engagements;
using ARC.App.Common.Mappings;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using ARC.App.Clients;

namespace ARC.Web.Models
{
    public class EngagementUpsertViewModel : IMapFrom<EngagementDetail>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int ClientId { get; set; }

        [MaxLength(100)]
        public string ManagerName { get; set; }

        [MaxLength(100)]
        public string PartnerName { get; set; }

        [MaxLength(100)]
        public string GroupName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FieldWorkEndDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ClientYearEndDate { get; set; }

        public string TeamEmailAddresses { get; set; }

        public IList<ClientLookupDto> Clients { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EngagementDetail, EngagementUpsertViewModel>();
            profile.CreateMap<EngagementUpsertViewModel, CreateEngagementCommand>();
            profile.CreateMap<EngagementUpsertViewModel, UpdateEngagementCommand>();
        }
    }
}
