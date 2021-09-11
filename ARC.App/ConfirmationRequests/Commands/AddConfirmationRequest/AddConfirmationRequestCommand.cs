using System.Threading;
using System.Threading.Tasks;
using ARC.Domain;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App
{
    public class AddConfirmationRequestCommand : IRequest<int>
    {
        public int AuthorizationRequestId { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string InvoiceId { get; set; }
        public decimal BalanceAmount { get; set; }

        public class AddConfirmationRequestCommandHandler : IRequestHandler<AddConfirmationRequestCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
            private readonly IDocumentService<ConfirmationRequest> _documentService;

            public AddConfirmationRequestCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory,
                IDocumentService<ConfirmationRequest> documentService)
            {
                _dbFactory = dbFactory;
                _documentService = documentService;
            }

            public async Task<int> Handle(AddConfirmationRequestCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();
                var entity = new ConfirmationRequest
                {
                    AuthorizationRequestId = request.AuthorizationRequestId,
                    ContactName = request.ContactName,
                    ContactEmail = request.ContactEmail,
                    InvoiceId = request.InvoiceId,
                    BalanceAmount = request.BalanceAmount,
                };

                await _documentService.CreateAsync(entity);

                context.ConfirmationRequests.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
