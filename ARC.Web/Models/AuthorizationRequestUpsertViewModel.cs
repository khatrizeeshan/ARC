using ARC.App.AuthorizationRequests;
using ARC.App.Common.Mappings;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ARC.App.Engagements;

namespace ARC.Web.Models
{
    public class AuthorizationRequestUpsertViewModel : IMapFrom<AuthorizationRequestDetail>
    {
        public int Id { get; set; }

        [Required]
        public int EngagementId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ContactName { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string ContactEmail { get; set; }

        [Required]
        public int MaximumReminders { get; set; }

        public bool HasSent { get; set; }

        public IList<EngagementLookupDto> Engagements { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthorizationRequestDetail, AuthorizationRequestUpsertViewModel>();
            profile.CreateMap<AuthorizationRequestUpsertViewModel, CreateAuthorizationRequestCommand>();
            profile.CreateMap<AuthorizationRequestUpsertViewModel, UpdateAuthorizationRequestCommand>();
        }
    }
}
