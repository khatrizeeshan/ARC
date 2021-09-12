using System.Threading;
using System.Threading.Tasks;
using ARC.App.Common;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App.Clients
{
    public class DeleteClientCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

            public DeleteClientCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory)
            {
                _dbFactory = dbFactory;
            }

            public async Task<int> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();

                var entity = await context.Clients
                    .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Client), request.Id);
                }

                context.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
