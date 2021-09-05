using Microsoft.Extensions.DependencyInjection;
using System;

namespace ARC.Persistance
{
    public static class DbInitializer
    {
        public static IServiceProvider InitializeDb(this IServiceProvider provider)
        {
            var context = provider.GetRequiredService<ARCContext>();
            context.Database.EnsureCreated();

            return provider;
        }
    }
}
