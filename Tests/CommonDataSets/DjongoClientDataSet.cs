using DataAccessLayer.Clients.DjangoClientDomain.Models;
using DjangoClientLib.Models.Contracts;

namespace CommonDataSets
{
    public class DjongoClientDataSet
    {
        const string _ApiUrl = "https://swapi.py4e.com/api/";

        public static Uri GetPersonRootUri()
            => new Uri($@"{_ApiUrl}{ApiTypes.People}/");

        public ModelRecord<Person> Person1;
        public ModelRecord<Vehicle> Vehicle1;
        public ModelRecord<Starship> Starship1;
        public ModelRecord<Starship> Starship2;
        public ModelRecord<Film> Film1;

        public DjongoClientDataSet()
        {
            SetupPersonsInit();
            SetupUrls();
            SetupPersonsAddReferences();
        }

        static class ApiTypes
        {
            public const string Film = "films";
            public const string People = "people";
            public const string Starship = "starships";
            public const string Vehicle = "vehicles";
        }

        public class ModelRecord<T>
            where T : IUrl
        {
            public string Type { get; set; }
            public int Id { get; set; }
            public T Model { get; set; }

            public Uri GetUrl()
            {
                return new Uri($"{_ApiUrl}{Type}/{Id}/");
            }
        }

        private void SetupPersonsInit()
        {
            Person1 = new ModelRecord<Person>
            {
                Type = ApiTypes.People,
                Id = 11,
                Model = new Person
                {
                    Name = "Tom Hanks",
                }
            };
            Vehicle1 = new ModelRecord<Vehicle>
            {
                Type = ApiTypes.Vehicle,
                Id = 101,
                Model = new Vehicle
                {
                    Name = "Opel Astra",
                }
            };
            Starship1 = new ModelRecord<Starship>
            {
                Type = ApiTypes.Starship,
                Id = 201,
                Model = new Starship
                {
                    Name = "Apollo 8",
                }
            };
            Starship2 = new ModelRecord<Starship>
            {
                Type = ApiTypes.Starship,
                Id = 202,
                Model = new Starship
                {
                    Name = "Apollo 13",
                }
            };
            Film1 = new ModelRecord<Film>
            {
                Type = ApiTypes.Film,
                Id = 301,
                Model = new Film
                {
                    Title = "Apollo 13",
                }
            };
        }

        private void SetupUrls()
        {
            UrlSetter(Person1);
            UrlSetter(Vehicle1);
            UrlSetter(Starship1);
            UrlSetter(Starship2);
            UrlSetter(Film1);
        }

        private void UrlSetter<T>(ModelRecord<T> obj)
            where T : IUrl
        {
            obj.Model.Url = obj.GetUrl();
        }

        private void SetupPersonsAddReferences()
        {
            Person1.Model.Vehicles = [Vehicle1.Model.Url];
            Person1.Model.Starships = [Starship1.Model.Url, Starship2.Model.Url];
            Person1.Model.Films = [Film1.Model.Url];
        }
    }
}
