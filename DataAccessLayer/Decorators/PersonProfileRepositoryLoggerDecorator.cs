using CommonComponents.Logs;
using DataAccessLayer.Models;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Decorators
{
    public class PersonProfileRepositoryLoggerDecorator : BasePerformanceLoggerDecorator<IPersonProfileRepository>, IPersonProfileRepository
    {
        IPersonProfileRepository _Service;

        public PersonProfileRepositoryLoggerDecorator(
            IPersonProfileRepository service,
            ILogger<PersonProfileRepositoryLoggerDecorator> logger)
            : base(logger)
        {
            _Service = service ?? throw new ArgumentNullException(nameof(service));
        }
        public async Task<PersonProfile> GetPersonProfile(int personId)
        {
            var method = async () => { return await _Service.GetPersonProfile(personId); };
            var parameters = new Dictionary<string, object> { { nameof(personId), personId } };
            return await ExecuteFunction(
                nameof(GetPersonProfile), 
                method
                );
        }
    }
}
