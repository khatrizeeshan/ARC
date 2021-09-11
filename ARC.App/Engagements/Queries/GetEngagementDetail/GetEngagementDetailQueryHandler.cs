using ARC.App.Engagements;
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

namespace ARC.App.Engagements
{
    public class GetEngagementDetailQueryHandler : IRequestHandler<GetEngagementDetailQuery, EngagementDetail>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IMapper _mapper;

        public GetEngagementDetailQueryHandler(IDbContextFactory<ApplicationDbContext> dbFactory, IMapper mapper)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public async Task<EngagementDetail> Handle(GetEngagementDetailQuery request, CancellationToken cancellationToken)
        {
            using var context = _dbFactory.CreateDbContext();

            var entity = await context.Engagements
                .Where(e => e.Id == request.Id)
                .ProjectTo<EngagementDetail>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if(entity == null)
            {
                throw new NotFoundException(nameof(Engagement), request.Id);
            }

            return entity;
        }
    }
}
