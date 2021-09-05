using ARC.Domain;
using Microsoft.EntityFrameworkCore;

namespace ARC.Persistance
{
    public class ARCContext : DbContext
    {
        public ARCContext(DbContextOptions<ARCContext> options) : base(options) 
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Engagement> Engagements { get; set; }
        public DbSet<AuthorizationRequest> AuthorizationRequests { get; set; }
        public DbSet<ConfirmationRequest> ConfirmationRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SetDefaultPrecision(18, 2);

            modelBuilder.ApplyConfiguration(new ClientConfig());
            modelBuilder.ApplyConfiguration(new EngagementConfig());
            modelBuilder.ApplyConfiguration(new AuthorizationRequestConfig());
            modelBuilder.ApplyConfiguration(new ConfirmationRequestConfig());
        }

    }
}