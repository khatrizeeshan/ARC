using System.Threading;
using System.Threading.Tasks;
using ARC.App.Common;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App.Engagements
{
    public class DeleteEngagementCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteEngagementCommandHandler : IRequestHandler<DeleteEngagementCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

            public DeleteEngagementCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory)
            {
                _dbFactory = dbFactory;
            }

            public async Task<int> Handle(DeleteEngagementCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();

                var entity = await context.Engagements
                    .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Engagement), request.Id);
                }

                context.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
