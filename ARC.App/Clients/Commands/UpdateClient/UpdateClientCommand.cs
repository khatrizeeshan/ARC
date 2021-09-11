using System.Threading;
using System.Threading.Tasks;
using ARC.App.Common;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App.Clients
{
    public class UpdateClientCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Industry { get; set; }

        public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

            public UpdateClientCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory)
            {
                _dbFactory = dbFactory;
            }

            public async Task<int> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();

                var entity = await context.Clients
                    .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Client), request.Id);
                }

                entity.Code = request.Code;
                entity.Name = request.Name;
                entity.Industry = request.Industry;

                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
