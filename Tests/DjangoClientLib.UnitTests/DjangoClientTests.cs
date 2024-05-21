using CommonDataSets;
using DataAccessLayer.Clients.DjangoClientEntities;
using DjangoClientLib.Models.Contracts;
using Moq;
using RichardSzalay.MockHttp;
using static CommonDataSets.DjongoClientDataSet;

namespace DjangoClientLib.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class DjangoClientTests
    {
        DjongoClientDataSet _DjongoClientDataSet;
        DjangoClient _Client;

        [SetUp]
        public void Setup()
        {
            _DjongoClientDataSet = new DjongoClientDataSet();
            SetupClient();
        }

        [Test]
        public async Task GetPersonAsyncTest()
        {
            var response = await _Client.GetPersonAsync(_DjongoClientDataSet.Person1.Id);

            Assert.That(
                StandardJsonSerializer(response),
                Is.EqualTo(_DjongoClientDataSet.Person1.ModelAsJson()));
        }

        [Test]
        public async Task GetFilmsAsyncTest()
        {
            var response = await _Client.GetFilmsAsync(_DjongoClientDataSet.Film1.Model.Url);

            Assert.That(
                StandardJsonSerializer(response), 
                Is.EqualTo(_DjongoClientDataSet.Film1.ModelAsJson()));
        }

        [Test]
        public async Task GetVehiclesAsync()
        {
            var response = await _Client.GetVehiclesAsync(_DjongoClientDataSet.Vehicle1.Model.Url);

            Assert.That(
                StandardJsonSerializer(response),
                Is.EqualTo(_DjongoClientDataSet.Vehicle1.ModelAsJson()));
        }

        [Test]
        public async Task GetStarshipsAsync()
        {
            var response = await _Client.GetStarshipsAsync(_DjongoClientDataSet.Starship1.Model.Url);

            Assert.That(
                StandardJsonSerializer(response),
                Is.EqualTo(_DjongoClientDataSet.Starship1.ModelAsJson()));
        }

        #region Helpers
        private void SetupClient()
        {
            var mockHttp = new MockHttpMessageHandler();

            RequestResponseMockSetter(mockHttp, _DjongoClientDataSet.Person1);
            RequestResponseMockSetter(mockHttp, _DjongoClientDataSet.Vehicle1);
            RequestResponseMockSetter(mockHttp, _DjongoClientDataSet.Starship1);
            RequestResponseMockSetter(mockHttp, _DjongoClientDataSet.Starship2);
            RequestResponseMockSetter(mockHttp, _DjongoClientDataSet.Film1);

            var httpClient = new HttpClient(mockHttp);

            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(library => library.CreateClient(DjangoClient.ClientName))
                .Returns(httpClient);

            _Client = new DjangoClient(
                factoryMock.Object,
                new DjangoClientConfig(GetPersonRootUri()));
        }

        private void RequestResponseMockSetter<T>(MockHttpMessageHandler mockHttp, ModelRecord<T> model)
            where T : IUrl
        {
            mockHttp.When(HttpMethod.Get, model.Model.Url.ToString())
                .Respond("application/json", model.ModelAsJson());
        }
        #endregion Helpers
    }
}