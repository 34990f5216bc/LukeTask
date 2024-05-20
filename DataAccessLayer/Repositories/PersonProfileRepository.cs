using DataAccessLayer.Clients.DjangoClientEntities;
using DataAccessLayer.Models;
using System.Collections.Concurrent;


namespace DataAccessLayer.Repositories
{
    public class PersonProfileRepository : IPersonProfileRepository
    {
        IDjangoClient _DjangoClient;
        public PersonProfileRepository(IDjangoClient djangoClient) 
        {
            _DjangoClient = djangoClient ?? throw new ArgumentNullException(nameof(djangoClient));
        }

        public async Task<PersonProfile> GetPersonProfile(int personId)
        {
            var person = await _DjangoClient.GetPersonAsync(personId);
            var getFilmsTask = GetFilmsTitles(person.Films);
            var getVehiclesTask = GetVehiclesNames(person.Vehicles);
            var getStarshipsTask = GetStarshipsNames(person.Starships);

            var personProfileResult = new PersonProfile
            {
                FullName = person.Name,
                Films = await getFilmsTask,
                Vehicles = await getVehiclesTask,
                Starships = await getStarshipsTask,
            };

            return personProfileResult;
        }

        private async Task<string[]> GetFilmsTitles(IEnumerable<Uri> urls)
        {
            var films = await RequestsStrategy(urls, _DjangoClient.GetFilmsAsync);
            return films.Select(x=>x.Title).ToArray();
        }

        private async Task<string[]> GetVehiclesNames(IEnumerable<Uri> urls)
        {
            var vehicles = await RequestsStrategy(urls, _DjangoClient.GetVehiclesAsync);
            return vehicles.Select(x => x.Name).ToArray();
        }

        private async Task<string[]> GetStarshipsNames(IEnumerable<Uri> urls)
        {
            var starships = await RequestsStrategy(urls, _DjangoClient.GetStarshipsAsync);
            return starships.Select(x => x.Name).ToArray();
        }

        private async Task<ConcurrentBag<T>> RequestsStrategy<T>(IEnumerable<Uri> urls, Func<Uri,Task<T>> requestFunc)
        {
            var resultBag = new ConcurrentBag<T>();
            await Parallel.ForEachAsync(urls, async (url, ct) => {
                var result = await requestFunc(url);
                resultBag.Add(result);
             });
            return resultBag;
        }

    }
}
