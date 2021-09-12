using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using ARC.App.Common;
using Microsoft.Extensions.Configuration;
using ARC.Persistance;
using ARC.Infrastructure;

namespace ARC.App
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistance(configuration);
            services.AddInfrastructure(configuration);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
