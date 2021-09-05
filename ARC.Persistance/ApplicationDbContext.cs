using ARC.Domain;
using Microsoft.EntityFrameworkCore;

namespace ARC.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Engagement> Engagements { get; set; }
        public DbSet<AuthorizationRequest> AuthorizationRequests { get; set; }
        public DbSet<ConfirmationRequest> ConfirmationRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.AddSequencesForEntities();
            builder.SetDefaultPrecision(18, 2);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

    }
}