using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ARC.App.Common;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App.AuthorizationRequests
{
    public class UpdateAuthorizationRequestCommand : IRequest<int>
    {
        public int Id { get; set; }

        public int EngagementId { get; set; }

        public int MaximumReminders { get; set; }

        public string ContactName { get; set; }

        public string ContactEmail { get; set; }

        public class UpdateAuthorizationRequestCommandHandler : IRequestHandler<UpdateAuthorizationRequestCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

            public UpdateAuthorizationRequestCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory)
            {
                _dbFactory = dbFactory;
            }

            public async Task<int> Handle(UpdateAuthorizationRequestCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();

                var entity = await context.AuthorizationRequests
                    .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(AuthorizationRequest), request.Id);
                }

                if (entity.HasSent)
                {
                    throw new BadRequestException("Request has sent, cannot update.");
                }

                entity.EngagementId = request.EngagementId;
                entity.MaximumReminders = request.MaximumReminders;
                entity.ContactName = request.ContactName;
                entity.ContactEmail = request.ContactEmail;

                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
