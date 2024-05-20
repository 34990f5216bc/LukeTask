using ServiceLayer.SavePathProviderEntities;
using System.IO.Abstractions.TestingHelpers;

namespace ServiceLayer.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class SavePathProviderTests
    {
        MockFileSystem _MockFileSystem = new MockFileSystem();

        [OneTimeSetUp]
        public void SetUp()
        {
            _MockFileSystem = new MockFileSystem();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("xxx?xxx")]
        public void SavePath_BadFileNameTest(string path)
        {
            var config = new SavePathProvider(new SavePathConfig(path), _MockFileSystem);

            Assert.IsNotNull(config.SavePath);
            Assert.That(
                Path.GetDirectoryName(config.SavePath),
                Is.EqualTo(_MockFileSystem.Directory.GetCurrentDirectory()));
            Assert.That(
                Path.GetExtension(config.SavePath),
                Is.EqualTo(".txt"));
        }

        [Test]
        [TestCase("someFile")]
        [TestCase("someFile.json")]
        [TestCase("someFile.txt")]
        public void SavePath_GoodFilePathProvidedTest(string fileName)
        {
            var config = new SavePathProvider(new SavePathConfig(fileName), _MockFileSystem);

            Assert.IsNotNull(config.SavePath);
            Assert.That(
                Path.GetDirectoryName(config.SavePath),
                Is.EqualTo(_MockFileSystem.Directory.GetCurrentDirectory()));
            Assert.That(
                Path.GetFileName(config.SavePath),
                Is.EqualTo(fileName));
        }
    }
}
