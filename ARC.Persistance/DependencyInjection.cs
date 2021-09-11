using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ARC.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContextFactory<ApplicationDbContext>(options =>
            {
#if DEBUG
                options.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging();
#else
                options.UseSqlServer(connectionString);
#endif
            });

            services.AddScoped(p =>
                p.GetRequiredService<IDbContextFactory<ApplicationDbContext>>()
                .CreateDbContext());

            return services;
        }
    }
}
