using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;
using DataAccessLayer.Extensions;
using DjangoClientLib.Extensions;
using ServiceLayer.Extensions;
using ServiceLayer.SavePathProviderEntities;
using DjangoClientLib;
using Polly.Extensions.Http;
using Polly;
using DataAccessLayer.Clients.DjangoClientEntities;
using System.Net;

namespace LukeDownloader.Configuration
{
    public static class IocConfig
    {
        public static IServiceCollection ConfogureIoc(this IServiceCollection service, IConfiguration config)
        {
            service.RegisterDjangoClient();
            service.ConfigureFileSystem();
            service.RegisterServices();
            service.RegisterPersonProfileRepository();
            service.RegisterConfigs(config);
            service.AddHttpClient(DjangoClient.ClientName)
                .AddPolicyHandler(GetGeneralPolicy())
                .AddPolicyHandler(GetToManyRequestsPolicy());

            return service;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetGeneralPolicy()
        {
            return HttpPolicyExtensions
                     .HandleTransientHttpError()
                     .WaitAndRetryAsync(3, retryNum => TimeSpan.FromSeconds(retryNum));
        }

        private static IAsyncPolicy<HttpResponseMessage> GetToManyRequestsPolicy()
        {
            return Policy
                .HandleResult<HttpResponseMessage>(msg => msg.StatusCode == HttpStatusCode.TooManyRequests && msg.Headers.RetryAfter != null)
                .WaitAndRetryAsync(
                   3,
                   sleepDurationProvider: (_, result, _) => result.Result.Headers.RetryAfter.Delta.Value,
                   onRetryAsync: (_, _, _, _) => Task.CompletedTask);
        }

        public static IServiceCollection RegisterConfigs(this IServiceCollection service, IConfiguration config)
        {
            service.AddScoped(x => new SavePathConfig(config["OutputFile"]));
            service.AddScoped(x => new DjangoClientConfig(new Uri("https://swapi.py4e.com/api/people/")));
            return service;
        }

        private static IServiceCollection ConfigureFileSystem(this IServiceCollection service)
        {
            service.AddScoped<IFileSystem>(x => new FileSystem());
            return service;
        }
    }
}
