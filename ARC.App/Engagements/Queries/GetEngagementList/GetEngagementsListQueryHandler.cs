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
    public class GetEngagementsListQueryHandler : IRequestHandler<GetEngagementsListQuery, EngagementsList>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IMapper _mapper;

        public GetEngagementsListQueryHandler(IDbContextFactory<ApplicationDbContext> dbFactory, IMapper mapper)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public async Task<EngagementsList> Handle(GetEngagementsListQuery request, CancellationToken cancellationToken)
        {
            using var context = _dbFactory.CreateDbContext();

            var Engagements = await context.Engagements
                .ProjectTo<EngagementDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new EngagementsList
            {
                Engagements = Engagements
            };

            return vm;
        }
    }
}
