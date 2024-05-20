using CommonDataSets;
using DataAccessLayer.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ServiceLayer.PersonIdProviderEntities;
using ServiceLayer.PersonInfoDownloadServiceEntities;
using ServiceLayer.SavePathProviderEntities;
using System.IO.Abstractions.TestingHelpers;

namespace ServiceLayer.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class PersonInfoDownloadServiceTests
    {
        const string _Path = $@"C:\Data\TargetDir\file.json";
        MockFileSystem _FileSystem;
        DjongoClientDataSet _DjongoClientDataSet;
        PersonInfoDownloadService _PersonInfoDownloadService;
        PersonProfile _GetPersonProfileResult;

        [SetUp]
        public void Setup()
        {


            _DjongoClientDataSet = new DjongoClientDataSet();
         
            SetupFileSystem();
            SetupRepositoryResult();
            
            _PersonInfoDownloadService = new PersonInfoDownloadService(

                GetConfig(),
                _FileSystem,
                GetRepository(),
                new NullLogger<PersonInfoDownloadService>(),
                GetPersonalIdProvider()
            );
        }

        [Test]
        public async Task DownloadPersonDataTest()
        {
            await _PersonInfoDownloadService.DownloadPersonData();

            Assert.That(
                _FileSystem.AllFiles.Count(),
                Is.EqualTo(1));
            Assert.That(
                _FileSystem.GetFile(_Path).TextContents, 
                Is.EqualTo(_GetPersonProfileResult.ToJson()));
        }

        #region Helpers
        private void SetupFileSystem()
        {
            _FileSystem = new MockFileSystem();
            _FileSystem.AddDirectory(Path.GetDirectoryName(_Path));
        }

        private ISavePathProvider GetConfig()
        {
            var mockPersonInfoDownloadServiceConfig = new Mock<ISavePathProvider>();
            mockPersonInfoDownloadServiceConfig.Setup(library => library.SavePath)
                .Returns(_Path);
            return mockPersonInfoDownloadServiceConfig.Object;
        }

        private void SetupRepositoryResult()
        {
            _GetPersonProfileResult = new PersonProfile
            {
                FullName = _DjongoClientDataSet.Person1.Model.Name,
                Films = [_DjongoClientDataSet.Film1.Model.Title],
                Starships = [_DjongoClientDataSet.Starship1.Model.Name, _DjongoClientDataSet.Starship2.Model.Name],
                Vehicles = [_DjongoClientDataSet.Vehicle1.Model.Name]
            };
        }

        private IPersonProfileRepository GetRepository()
        {
            var mockPersonProfileRepository = new Mock<IPersonProfileRepository>();
            mockPersonProfileRepository.Setup(library => library.GetPersonProfile(_DjongoClientDataSet.Person1.Id))
                .ReturnsAsync(_GetPersonProfileResult);
            return mockPersonProfileRepository.Object;
        }

        private IPersonIdProvider GetPersonalIdProvider()
        {
            var mockPersonIdProvider = new Mock<IPersonIdProvider>();
            mockPersonIdProvider.Setup(library => library.GetPersonId())
                .Returns(_DjongoClientDataSet.Person1.Id);
            return mockPersonIdProvider.Object;
        }
        #endregion Helpers
    }
}