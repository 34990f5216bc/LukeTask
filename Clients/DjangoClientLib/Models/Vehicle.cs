using DjangoClientLib.JsonConverters;
using DjangoClientLib.Models;
using DjangoClientLib.Models.Contracts;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Clients.DjangoClientDomain.Models
{
    public class Vehicle : BaseModel, IUrl
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("manufacturer")]
        public string? Manufacturer { get; set; }

        [JsonPropertyName("cost_in_credits")]
        public string? CostInCredits { get; set; }

        [JsonPropertyName("length")]
        public string? Length { get; set; }

        [JsonPropertyName("max_atmosphering_speed")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MaxAtmospheringSpeed { get; set; }

        [JsonPropertyName("crew")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Crew { get; set; }

        [JsonPropertyName("passengers")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Passengers { get; set; }

        [JsonPropertyName("cargo_capacity")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CargoCapacity { get; set; }

        [JsonPropertyName("consumables")]
        public string? Consumables { get; set; }

        [JsonPropertyName("vehicle_class")]
        public string? VehicleClass { get; set; }

        [JsonPropertyName("pilots")]
        public Uri[]? Pilots { get; set; }

        [JsonPropertyName("films")]
        public Uri[]? Films { get; set; }

        [JsonPropertyName("created")]
        public DateTimeOffset Created { get; set; }

        [JsonPropertyName("edited")]
        public DateTimeOffset Edited { get; set; }

        [JsonPropertyName("url")]
        public Uri? Url { get; set; }
    }
}
