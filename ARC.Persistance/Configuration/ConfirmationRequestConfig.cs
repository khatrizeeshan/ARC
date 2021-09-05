using ARC.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistance
{
    public class ConfirmationRequestConfig : IEntityTypeConfiguration<ConfirmationRequest>
    {
        public void Configure(EntityTypeBuilder<ConfirmationRequest> builder)
        {
            
        }
    }
}