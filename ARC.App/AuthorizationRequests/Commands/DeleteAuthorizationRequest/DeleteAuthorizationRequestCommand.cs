using System.Threading;
using System.Threading.Tasks;
using ARC.App.Common;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App.AuthorizationRequests
{
    public class DeleteAuthorizationRequestCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Industry { get; set; }

        public class DeleteAuthorizationRequestCommandHandler : IRequestHandler<DeleteAuthorizationRequestCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

            public DeleteAuthorizationRequestCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory)
            {
                _dbFactory = dbFactory;
            }

            public async Task<int> Handle(DeleteAuthorizationRequestCommand request, CancellationToken cancellationToken)
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
                    throw new BadRequestException("Request has sent, cannot delete.");
                }

                context.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
