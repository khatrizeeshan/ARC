using ARC.App.Clients;
using ARC.Persistance;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ARC.App.Clients
{
    public class GetClientsLookupListQueryHandler : IRequestHandler<GetClientsLookupListQuery, ClientsLookupList>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IMapper _mapper;

        public GetClientsLookupListQueryHandler(IDbContextFactory<ApplicationDbContext> dbFactory, IMapper mapper)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public async Task<ClientsLookupList> Handle(GetClientsLookupListQuery request, CancellationToken cancellationToken)
        {
            using var context = _dbFactory.CreateDbContext();

            var clients = await context.Clients
                .ProjectTo<ClientLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ClientsLookupList
            {
                Clients = clients
            };

            return vm;
        }
    }
}
