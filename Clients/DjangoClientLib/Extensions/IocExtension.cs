using DataAccessLayer.Clients.DjangoClientEntities;
using Microsoft.Extensions.DependencyInjection;

namespace DjangoClientLib.Extensions
{
    public static class IocExtension
    {
        public static IServiceCollection RegisterDjangoClient(this IServiceCollection services)
        {
            services.AddScoped<IDjangoClient, DjangoClient>();
            return services;
        }
    }
}
