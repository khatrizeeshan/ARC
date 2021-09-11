using ARC.App.Clients;
using ARC.App.Common.Mappings;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace ARC.Web.Models
{
    public class ClientViewModel : IMapFrom<ClientDetail>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Industry { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ClientDetail, ClientViewModel>();
            //.ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            //.ForMember(d => d.Code, opt => opt.MapFrom(s => s.Code))
            //.ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            //.ForMember(d => d.Industry, opt => opt.MapFrom(s => s.Industry));
        }
    }
}
