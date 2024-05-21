using DjangoClientLib.JsonConverters;
using DjangoClientLib.Models.Contracts;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Clients.DjangoClientDomain.Models
{
    public class Person : IUrl
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("height")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Height { get; set; }

        [JsonPropertyName("mass")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Mass { get; set; }

        [JsonPropertyName("hair_color")]
        public string? HairColor { get; set; }

        [JsonPropertyName("skin_color")]
        public string? SkinColor { get; set; }

        [JsonPropertyName("eye_color")]
        public string? EyeColor { get; set; }

        [JsonPropertyName("birth_year")]
        public string? BirthYear { get; set; }

        [JsonPropertyName("gender")]
        public string? Gender { get; set; }

        [JsonPropertyName("homeworld")]
        public Uri? Homeworld { get; set; }

        [JsonPropertyName("films")]
        public Uri[]? Films { get; set; }

        [JsonPropertyName("species")]
        public Uri[]? Species { get; set; }

        [JsonPropertyName("vehicles")]
        public Uri[]? Vehicles { get; set; }

        [JsonPropertyName("starships")]
        public Uri[]? Starships { get; set; }

        [JsonPropertyName("created")]
        public DateTimeOffset Created { get; set; }

        [JsonPropertyName("edited")]
        public DateTimeOffset Edited { get; set; }

        [JsonPropertyName("url")]
        public Uri? Url { get; set; }

    }
}
