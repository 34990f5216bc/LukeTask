using CommonDataSets;
using DataAccessLayer.Clients.DjangoClientEntities;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessLayer.UnitTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class PersonProfileRepositoryTests
    {
        DjongoClientDataSet _DjongoClientDataSet;
        PersonProfileRepository _Repository { get; set; }

        [SetUp]
        public void Setup()
        {
            _DjongoClientDataSet = new DjongoClientDataSet();
            var mockedClient = GetMockedClient();
            _Repository = new PersonProfileRepository(mockedClient);
        }

        [Test]
        public async Task GetPersonProfileTest()
        {
            var expectedResult = new PersonProfile
            {
                FullName = _DjongoClientDataSet.Person1.Model.Name,
                Films = [_DjongoClientDataSet.Film1.Model.Title],
                Starships = [_DjongoClientDataSet.Starship2.Model.Name, _DjongoClientDataSet.Starship1.Model.Name],
                Vehicles = [_DjongoClientDataSet.Vehicle1.Model.Name]
            };

            var result = await _Repository.GetPersonProfile(_DjongoClientDataSet.Person1.Id);

            Assert.That(result, Is.EqualTo(expectedResult).Using(new PersonProfileCustomComparer()));
           
        }

        #region Helpers
        class PersonProfileCustomComparer : IEqualityComparer<PersonProfile>
        {
            public bool Equals(PersonProfile x, PersonProfile y)
            {
                return x.FullName == y.FullName
                    && FastEquivalent(x.Vehicles, x.Vehicles)
                    && FastEquivalent(x.Starships, x.Starships)
                    && FastEquivalent(x.Films, x.Films);
            }

            public int GetHashCode([DisallowNull] PersonProfile obj)
            {
                return obj.GetHashCode();
            }

            private bool FastEquivalent<T>(T[] collection1, T[] collection2)
            {
                if (collection1.Length != collection2.Length)
                    return false;

                var hashSet = collection1.ToHashSet();
                foreach (var rec in collection2)
                {
                    if (!hashSet.Contains(rec))
                        return false;
                }

                return true;
            }
        }

        private IDjangoClient GetMockedClient()
        {
            var mockedDjangoClient = new Mock<IDjangoClient>();
            mockedDjangoClient.Setup(x => x.GetStarshipsAsync(_DjongoClientDataSet.Starship1.Model.Url))
                .ReturnsAsync(_DjongoClientDataSet.Starship1.Model);

            mockedDjangoClient.Setup(x => x.GetStarshipsAsync(_DjongoClientDataSet.Starship2.Model.Url))
                .ReturnsAsync(_DjongoClientDataSet.Starship2.Model);

            mockedDjangoClient.Setup(x => x.GetVehiclesAsync(_DjongoClientDataSet.Vehicle1.Model.Url))
                .ReturnsAsync(_DjongoClientDataSet.Vehicle1.Model);

            mockedDjangoClient.Setup(x => x.GetFilmsAsync(_DjongoClientDataSet.Film1.Model.Url))
                .ReturnsAsync(_DjongoClientDataSet.Film1.Model);

            mockedDjangoClient.Setup(x => x.GetPersonAsync(_DjongoClientDataSet.Person1.Id))
                .ReturnsAsync(_DjongoClientDataSet.Person1.Model);

            return mockedDjangoClient.Object;
        }
        #endregion Helpers
    }
}