using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ARC.Domain;
using ARC.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ARC.App.Engagements
{
    public class CreateEngagementCommand : IRequest<int>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int ClientId { get; set; }

        public string ManagerName { get; set; }

        public string PartnerName { get; set; }

        public string GroupName { get; set; }

        public DateTime? FieldWorkEndDate { get; set; }

        public DateTime? ClientYearEndDate { get; set; }

        public IList<string> TeamEmailAddresses { get; set; }

        public class CreateEngagementCommandHandler : IRequestHandler<CreateEngagementCommand, int>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

            public CreateEngagementCommandHandler(IDbContextFactory<ApplicationDbContext> dbFactory)
            {
                _dbFactory = dbFactory;
            }

            public async Task<int> Handle(CreateEngagementCommand request, CancellationToken cancellationToken)
            {
                using var context = _dbFactory.CreateDbContext();

                var entity = new Engagement
                {
                    Code = request.Code,
                    Name = request.Name,
                    ClientId = request.ClientId,
                    ManagerName = request.ManagerName,
                    PartnerName = request.PartnerName,
                    GroupName = request.GroupName,
                    FieldWorkEndDate = request.FieldWorkEndDate,
                    ClientYearEndDate = request.ClientYearEndDate,
                };

                if (request.TeamEmailAddresses != null && request.TeamEmailAddresses.Any())
                {
                    entity.TeamEmailAddresses = string.Join(';', request.TeamEmailAddresses);
                }
                else
                {
                    entity.TeamEmailAddresses = null;
                }

                context.Engagements.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
