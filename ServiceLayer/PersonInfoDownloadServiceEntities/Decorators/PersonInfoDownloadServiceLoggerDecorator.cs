using CommonComponents.Logs;
using Microsoft.Extensions.Logging;

namespace ServiceLayer.PersonInfoDownloadServiceEntities.Decorators
{
    public class PersonInfoDownloadServiceLoggerDecorator : BasePerformanceLoggerDecorator<IPersonInfoDownloadService>, IPersonInfoDownloadService
    {
        IPersonInfoDownloadService _Service;

        public PersonInfoDownloadServiceLoggerDecorator(
            IPersonInfoDownloadService service,
            ILogger<PersonInfoDownloadServiceLoggerDecorator> logger) 
            : base(logger)
        {
            _Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task DownloadPersonData()
            => await ExecuteAction(nameof(DownloadPersonData), _Service.DownloadPersonData);
    }
}
