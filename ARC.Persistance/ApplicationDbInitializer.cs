using Microsoft.Extensions.DependencyInjection;
using System;

namespace ARC.Persistance
{
    public static class ApplicationDbInitializer
    {
        public static IServiceProvider InitializeDb(this IServiceProvider provider)
        {
            var context = provider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            return provider;
        }
    }
}
