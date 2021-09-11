using System.Threading;
using System.Threading.Tasks;
using ARC.Domain;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App
{
    public class AddAuthorizationRequestCommand : IRequest<int>
    {
        public int EngagementId { get; set; }

        public string ContactName { get; set; }

        public string ContactEmail { get; set; }

        public int MaximumReminders { get; set; }

        public class AddAuthorizationRequestCommandHandler : IRequestHandler<AddAuthorizationRequestCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
            private readonly IDocumentService<AuthorizationRequest> _documentService;

            public AddAuthorizationRequestCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory,
                IDocumentService<AuthorizationRequest> documentService)
            {
                _dbFactory = dbFactory;
                _documentService = documentService;
            }

            public async Task<int> Handle(AddAuthorizationRequestCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();
                var entity = new AuthorizationRequest
                {
                    EngagementId = request.EngagementId,
                    ContactName = request.ContactName,
                    ContactEmail = request.ContactEmail,
                    MaximumReminders = request.MaximumReminders,
                };

                await _documentService.CreateAsync(entity);

                context.AuthorizationRequests.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
