using DataAccessLayer.Models;
using Microsoft.Extensions.Logging;
using ServiceLayer.PersonIdProviderEntities;
using System.IO.Abstractions;
using ServiceLayer.SavePathProviderEntities;

namespace ServiceLayer.PersonInfoDownloadServiceEntities
{
    public class PersonInfoDownloadService : IPersonInfoDownloadService
    {
        ISavePathProvider _Config;
        IFileSystem _FileSystem;
        IPersonProfileRepository _PersonProfileRepository;
        ILogger _Logger;
        IPersonIdProvider _PersonIdProvider;

        public PersonInfoDownloadService(
            ISavePathProvider config,
            IFileSystem fileSystem,
            IPersonProfileRepository personProfileRepository,
            ILogger<PersonInfoDownloadService> logger,
            IPersonIdProvider personIdProvider
            )
        {
            _Config = config ?? throw new ArgumentNullException(nameof(config));
            _FileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
            _PersonProfileRepository = personProfileRepository ?? throw new ArgumentNullException(nameof(personProfileRepository));
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _PersonIdProvider = personIdProvider ?? throw new ArgumentNullException(nameof(personIdProvider));
        }

        public async Task DownloadPersonData()
        {
            var data = await _PersonProfileRepository.GetPersonProfile(_PersonIdProvider.GetPersonId());
            var strData = data.ToJson();
            _FileSystem.File.WriteAllText(_Config.SavePath, strData);
            _Logger.LogInformation("Data saved succsesfully, personName: {0}, path: {1}", data.FullName, _Config.SavePath);
        }
    }
}
