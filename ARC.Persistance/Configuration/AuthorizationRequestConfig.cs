using ARC.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ARC.Persistance
{
    public class AuthorizationRequestConfig : IEntityTypeConfiguration<AuthorizationRequest>
    {
        public void Configure(EntityTypeBuilder<AuthorizationRequest> builder)
        {
            
        }
    }
}