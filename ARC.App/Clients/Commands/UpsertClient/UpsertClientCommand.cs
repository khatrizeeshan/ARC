using System.Threading;
using System.Threading.Tasks;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App
{
    public class UpsertClientCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Industry { get; set; }

        public class UpsertClientCommandHandler : IRequestHandler<UpsertClientCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

            public UpsertClientCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory)
            {
                _dbFactory = dbFactory;
            }

            public async Task<int> Handle(UpsertClientCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();
                Client entity;

                if (request.Id.HasValue)
                {
                    entity = await context.Clients.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Client();
                    context.Clients.Add(entity);
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
