using System.Threading;
using System.Threading.Tasks;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App.Clients
{
    public class CreateClientCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Industry { get; set; }

        public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

            public CreateClientCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory)
            {
                _dbFactory = dbFactory;
            }

            public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();

                var entity = new Client
                {
                    Code = request.Code,
                    Name = request.Name,
                    Industry = request.Industry
                };

                context.Clients.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
