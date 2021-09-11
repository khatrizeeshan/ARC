using ARC.App.Common.Mappings;
using ARC.Domain;
using AutoMapper;

namespace ARC.App.Clients
{
    public class ClientDetail : IMapFrom<Client>
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientDetail>();
                //.ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                //.ForMember(d => d.Code, opt => opt.MapFrom(s => s.Code))
                //.ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                //.ForMember(d => d.Industry, opt => opt.MapFrom(s => s.Industry));
        }
    }
}
