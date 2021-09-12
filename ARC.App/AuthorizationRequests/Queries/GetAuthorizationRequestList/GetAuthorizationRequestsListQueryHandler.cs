using ARC.App.AuthorizationRequests;
using ARC.Persistance;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ARC.App.AuthorizationRequests
{
    public class GetAuthorizationRequestsListQueryHandler : IRequestHandler<GetAuthorizationRequestsListQuery, AuthorizationRequestsList>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        private readonly IMapper _mapper;

        public GetAuthorizationRequestsListQueryHandler(IDbContextFactory<ApplicationDbContext> dbFactory, IMapper mapper)
        {
            _dbFactory = dbFactory;
            _mapper = mapper;
        }

        public async Task<AuthorizationRequestsList> Handle(GetAuthorizationRequestsListQuery request, CancellationToken cancellationToken)
        {
            using var context = _dbFactory.CreateDbContext();

            var AuthorizationRequests = await context.AuthorizationRequests
                .ProjectTo<AuthorizationRequestDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var vm = new AuthorizationRequestsList
            {
                AuthorizationRequests = AuthorizationRequests
            };

            return vm;
        }
    }
}
