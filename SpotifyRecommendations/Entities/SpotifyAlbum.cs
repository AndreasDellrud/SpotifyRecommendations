using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpotifyRecommendations.Entities
{
    public class SpotifyAlbum : SpotifyItemBase
    {
        [JsonProperty(PropertyName = "album_group")]
        public string AlbumGroup { get; set; }

        [JsonProperty(PropertyName = "album_type")]
        public string AlbumType { get; set; }


        [JsonProperty(PropertyName = "available_markets")]
        public List<string> AvailableMarkets { get; set; }

        [JsonProperty(PropertyName = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty(PropertyName = "release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty(PropertyName = "release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        public List<SpotifyArtist> Artists { get; set; }
        public List<SpotifyImage> Images { get; set; }
        public List<SpotifyTrack> Tracks { get; set; }
    }
}