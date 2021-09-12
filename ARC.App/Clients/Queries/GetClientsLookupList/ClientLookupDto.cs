using ARC.App.Common.Mappings;
using ARC.Domain;
using AutoMapper;

namespace ARC.App.Clients
{
    public class ClientLookupDto : IMapFrom<Client>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientLookupDto>();
        }
    }
}
