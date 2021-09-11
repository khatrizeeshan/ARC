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
    public class GetClientsListQueryHandler : IRequestHandler<GetClientsListQuery, ClientsList>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IMapper _mapper;

        public GetClientsListQueryHandler(IDbContextFactory<ApplicationDbContext> dbFactory, IMapper mapper)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public async Task<ClientsList> Handle(GetClientsListQuery request, CancellationToken cancellationToken)
        {
            using var context = _dbFactory.CreateDbContext();

            var clients = await context.Clients
                .ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new ClientsList
            {
                Clients = clients
            };

            return vm;
        }
    }
}
