using ARC.App.AuthorizationRequests;
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

namespace ARC.App.AuthorizationRequests
{
    public class GetAuthorizationRequestDetailQueryHandler : IRequestHandler<GetAuthorizationRequestDetailQuery, AuthorizationRequestDetail>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IMapper _mapper;

        public GetAuthorizationRequestDetailQueryHandler(IDbContextFactory<ApplicationDbContext> dbFactory, IMapper mapper)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public async Task<AuthorizationRequestDetail> Handle(GetAuthorizationRequestDetailQuery request, CancellationToken cancellationToken)
        {
            using var context = _dbFactory.CreateDbContext();

            var entity = await context.AuthorizationRequests
                .Where(e => e.Id == request.Id)
                .ProjectTo<AuthorizationRequestDetail>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if(entity == null)
            {
                throw new NotFoundException(nameof(AuthorizationRequest), request.Id);
            }

            return entity;
        }
    }
}
