using ARC.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistance
{
    public class EventLogConfig : IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
            builder.HasIndex(e => new { e.AssociatedWith, e.AssociatedWithId });
        }
    }
}