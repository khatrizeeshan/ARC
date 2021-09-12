using ARC.App.Engagements;
using ARC.Persistance;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ARC.App.Engagements
{
    public class GetEngagementsLookupListQueryHandler : IRequestHandler<GetEngagementsLookupListQuery, EngagementsLookupList>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IMapper _mapper;

        public GetEngagementsLookupListQueryHandler(IDbContextFactory<ApplicationDbContext> dbFactory, IMapper mapper)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public async Task<EngagementsLookupList> Handle(GetEngagementsLookupListQuery request, CancellationToken cancellationToken)
        {
            using var context = _dbFactory.CreateDbContext();

            var clients = await context.Engagements
                .ProjectTo<EngagementLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new EngagementsLookupList
            {
                Engagements = clients
            };

            return vm;
        }
    }
}
