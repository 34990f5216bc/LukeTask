using DataAccessLayer.Clients.DjangoClientDomain.Models;

namespace DataAccessLayer.Clients.DjangoClientEntities
{
    public interface IDjangoClient
    {
        Task<Person?> GetPersonAsync(int id);
        Task<Film?> GetFilmsAsync(Uri url);
        Task<Vehicle?> GetVehiclesAsync(Uri url);
        Task<Starship?> GetStarshipsAsync(Uri url);
    }
}
