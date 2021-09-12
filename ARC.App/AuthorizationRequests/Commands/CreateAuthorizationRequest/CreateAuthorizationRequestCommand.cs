using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App.AuthorizationRequests
{
    public class CreateAuthorizationRequestCommand : IRequest<int>
    {
        public int EngagementId { get; set; }

        public int MaximumReminders { get; set; }

        public string ContactName { get; set; }

        public string ContactEmail { get; set; }

        public class CreateAuthorizationRequestCommandHandler : IRequestHandler<CreateAuthorizationRequestCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

            public CreateAuthorizationRequestCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory)
            {
                _dbFactory = dbFactory;
            }

            public async Task<int> Handle(CreateAuthorizationRequestCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();

                var entity = new AuthorizationRequest
                {
                    EngagementId = request.EngagementId,
                    MaximumReminders = request.MaximumReminders,
                    ContactName = request.ContactName,
                    ContactEmail = request.ContactEmail,
                };

                context.AuthorizationRequests.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
