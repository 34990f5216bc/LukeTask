using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.PersonIdProviderEntities;
using ServiceLayer.PersonInfoDownloadServiceEntities;
using ServiceLayer.SavePathProviderEntities;

namespace ServiceLayer.Extensions
{
    public static class IocExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection service)
        {
            service.AddScoped<IPersonInfoDownloadService, PersonInfoDownloadService>();
            service.AddScoped<ISavePathProvider, SavePathProvider>();
            service.AddScoped<IPersonIdProvider, PersonIdProvider>();
            return service;
        }
    }
}
