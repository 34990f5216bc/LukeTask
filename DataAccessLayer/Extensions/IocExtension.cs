using DataAccessLayer.Decorators;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Extensions
{
    public static class IocExtension
    {
        public static IServiceCollection RegisterPersonProfileRepository(this IServiceCollection services)
        {
            services.AddScoped<IPersonProfileRepository, PersonProfileRepository>();
            services.Decorate<IPersonProfileRepository, PersonProfileRepositoryLoggerDecorator>();
            return services;
        }
    }
}
