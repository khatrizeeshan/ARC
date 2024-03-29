﻿using ARC.App.Common.Mappings;
using ARC.Domain;
using AutoMapper;

namespace ARC.App.Clients
{
    public class ClientDto : IMapFrom<Client>
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientDto>();
        }
    }
}
