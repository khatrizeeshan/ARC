using System.Threading;
using System.Threading.Tasks;
using ARC.App.Common;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App.AuthorizationRequests
{
    public class SendAuthorizationRequestCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class SendAuthorizationRequestCommandHandler : IRequestHandler<SendAuthorizationRequestCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
            private readonly IDocumentService<AuthorizationRequest> _service;

            public SendAuthorizationRequestCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory, IDocumentService<AuthorizationRequest> service)
            {
                _dbFactory = dbFactory;
                _service = service;
            }

            public async Task<int> Handle(SendAuthorizationRequestCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();

                var entity = await context.AuthorizationRequests
                    .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(AuthorizationRequest), request.Id);
                }

                if(entity.HasSent)
                {
                    throw new BadRequestException("Request has been sent, cannot be sent again.");
                }

                await _service.CreateAsync(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
