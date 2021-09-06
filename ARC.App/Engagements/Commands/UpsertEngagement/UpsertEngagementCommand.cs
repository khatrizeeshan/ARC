using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App
{
    public class UpsertEngagementCommand : IRequest<int>
    {
        public int? Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ManagerName { get; set; }

        public string PartnerName { get; set; }

        public string GroupName { get; set; }

        public DateTime FieldWorkEndDate { get; set; }

        public DateTime ClientYearEndDate { get; set; }

        public string[] TeamEmailAddresses { get; set; }

        public class UpsertEngagementCommandHandler : IRequestHandler<UpsertEngagementCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

            public UpsertEngagementCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory)
            {
                _dbFactory = dbFactory;
            }

            public async Task<int> Handle(UpsertEngagementCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();
                Engagement entity;

                if (request.Id.HasValue)
                {
                    entity = await context.Engagements.FindAsync(request.Id.Value);
                }
                else
                {
                    entity = new Engagement();
                    context.Engagements.Add(entity);
                }

                entity.Code = request.Code;
                entity.Name = request.Name;
                entity.ManagerName = request.ManagerName;
                entity.PartnerName = request.PartnerName;
                entity.GroupName = request.GroupName;
                entity.FieldWorkEndDate = request.FieldWorkEndDate;
                entity.ClientYearEndDate = request.ClientYearEndDate;
                entity.TeamEmailAddresses = string.Join(',', request.TeamEmailAddresses);

                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
