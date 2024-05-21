using DjangoClientLib.Models.Contracts;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Clients.DjangoClientDomain.Models
{
    public partial class Film : IUrl
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("episode_id")]
        public long EpisodeId { get; set; }

        [JsonPropertyName("opening_crawl")]
        public string? OpeningCrawl { get; set; }

        [JsonPropertyName("director")]
        public string? Director { get; set; }

        [JsonPropertyName("producer")]
        public string? Producer { get; set; }

        [JsonPropertyName("release_date")]
        public DateTimeOffset ReleaseDate { get; set; }

        [JsonPropertyName("characters")]
        public Uri[]? Characters { get; set; }

        [JsonPropertyName("planets")]
        public Uri[]? Planets { get; set; }

        [JsonPropertyName("starships")]
        public Uri[]? Starships { get; set; }

        [JsonPropertyName("vehicles")]
        public Uri[]? Vehicles { get; set; }

        [JsonPropertyName("species")]
        public Uri[]? Species { get; set; }

        [JsonPropertyName("created")]
        public DateTimeOffset Created { get; set; }

        [JsonPropertyName("edited")]
        public DateTimeOffset Edited { get; set; }

        [JsonPropertyName("url")]
        public Uri? Url { get; set; }
    }
}
