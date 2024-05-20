using DataAccessLayer.Clients.DjangoClientDomain.Models;
using DjangoClientLib;

namespace DataAccessLayer.Clients.DjangoClientEntities
{
    public class DjangoClient : IDjangoClient
    {
        public const string ClientName = "DjangoHttpClient";
        IHttpClientFactory _HttpClientFactory;
        DjangoClientConfig _DjangoClientConfig;

        public DjangoClient(
            IHttpClientFactory httpClientFactory,
            DjangoClientConfig djangoClientConfig)
        {
            _HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _DjangoClientConfig = djangoClientConfig ?? throw new ArgumentNullException(nameof(djangoClientConfig));
        }

        public async Task<Person?> GetPersonAsync(int personId)
            => await HandleRequestAsync<Person>(new Uri($@"{_DjangoClientConfig.PersonRootUrl}{personId}/"));

        public async Task<Film?> GetFilmsAsync(Uri url)
            => await HandleRequestAsync<Film>(url);

        public async Task<Vehicle?> GetVehiclesAsync(Uri url)
            => await HandleRequestAsync<Vehicle>(url);

        public async Task<Starship?> GetStarshipsAsync(Uri url)
            => await HandleRequestAsync<Starship>(url);

        private async Task<T?> HandleRequestAsync<T>(Uri url)
        {
            var client = _HttpClientFactory.CreateClient(ClientName);
            var response = await client.GetAsync(url);
            var stream = await response.Content.ReadAsStreamAsync();
            var obj = await System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream);
            return obj;
        }
    }
}
