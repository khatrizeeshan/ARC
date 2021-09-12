using ARC.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ARC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var apikey = configuration["PandaDoc:ApiKey"];
            services.AddHttpClient("pandadoc", c =>
            {
                c.BaseAddress = new Uri("https://api.pandadoc.com/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
                c.DefaultRequestHeaders.Add("Authorization", $"Api-Key {apikey}");
            });

            services.AddTransient<IDocumentService<AuthorizationRequest>, AuthorizationRequestDocumentService>();

            return services;
        }
    }
}
