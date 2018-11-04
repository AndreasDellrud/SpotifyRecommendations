using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpotifyRecommendations.Entities
{
    public class SpotifyTrack
    {
        public string AlbumId { get; set; }
        public SpotifyAlbum Album { get; set; }
        public List<SpotifyArtist> Artists { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
        public SpotifyAudioFeature AudioFeature { get; set; }

        [JsonProperty(PropertyName = "available_markets")]
        public List<string> AvailableMarkets { get; set; }

        [JsonProperty(PropertyName = "disc_number")]
        public int DiscNumber { get; set; }

        [JsonProperty(PropertyName = "duration_ms")]
        public int DurationMs { get; set; }

        public bool Explicit { get; set; }
        [JsonProperty(PropertyName = "external_ids")]
        public ExternalIds ExternalIds { get; set; }

        [JsonProperty(PropertyName = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty(PropertyName = "preview_url")]
        public string PreviewUrl { get; set; }

        [JsonProperty(PropertyName = "track_number")]
        public int TrackNumber { get; set; }
    }
}
