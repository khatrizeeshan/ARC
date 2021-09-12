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

namespace ARC.App.Engagements
{
    public class UpdateEngagementCommand : IRequest<int>
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int ClientId { get; set; }

        public string ManagerName { get; set; }

        public string PartnerName { get; set; }

        public string GroupName { get; set; }

        public DateTime? FieldWorkEndDate { get; set; }

        public DateTime? ClientYearEndDate { get; set; }

        public IList<string> TeamEmailAddresses { get; set; }

        public class UpdateEngagementCommandHandler : IRequestHandler<UpdateEngagementCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

            public UpdateEngagementCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory)
            {
                _dbFactory = dbFactory;
            }

            public async Task<int> Handle(UpdateEngagementCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();

                var entity = await context.Engagements
                    .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Engagement), request.Id);
                }

                entity.Code = request.Code;
                entity.Name = request.Name;
                entity.ClientId = request.ClientId;
                entity.ManagerName = request.ManagerName;
                entity.PartnerName = request.PartnerName;
                entity.GroupName = request.GroupName;
                entity.FieldWorkEndDate = request.FieldWorkEndDate;
                entity.ClientYearEndDate = request.ClientYearEndDate;

                if (request.TeamEmailAddresses != null && request.TeamEmailAddresses.Any())
                {
                    entity.TeamEmailAddresses = string.Join(';', request.TeamEmailAddresses);
                }
                else
                {
                    entity.TeamEmailAddresses = null;
                }

                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
