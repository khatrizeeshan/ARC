using ARC.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistance
{
    public class EngagementConfig : IEntityTypeConfiguration<Engagement>
    {
        public void Configure(EntityTypeBuilder<Engagement> builder)
        {
        }
    }
}