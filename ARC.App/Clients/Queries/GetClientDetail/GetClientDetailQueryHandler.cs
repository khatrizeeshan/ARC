using ARC.App.Clients;
using ARC.App.Common;
using ARC.Domain;
using ARC.Persistance;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ARC.App.Clients
{
    public class GetClientDetailQueryHandler : IRequestHandler<GetClientDetailQuery, ClientDetail>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IMapper _mapper;

        public GetClientDetailQueryHandler(IDbContextFactory<ApplicationDbContext> dbFactory, IMapper mapper)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public async Task<ClientDetail> Handle(GetClientDetailQuery request, CancellationToken cancellationToken)
        {
            using var context = _dbFactory.CreateDbContext();

            var entity = await context.Clients
                .Where(e => e.Id == request.Id)
                .ProjectTo<ClientDetail>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Client), request.Id);
            }

            return entity;
        }
    }
}
