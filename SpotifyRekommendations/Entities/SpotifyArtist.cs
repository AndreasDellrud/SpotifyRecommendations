using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyRekommendations.Entities
{
    public class SpotifyArtist
    {
        [JsonConstructor]
        public SpotifyArtist(List<string> genres)
        {
            Genres = genres.Select(x => new SpotifyGenre { Name = x }).ToList();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
        public int Popularity { get; set; }
        public ExternalUrls ExternalUrls { get; set; }
        public Followers Followers { get; set; }
        public List<SpotifyGenre> Genres { get; set; }
        public List<SpotifyImage> Images { get; set; }
    }

    public class ExternalUrls
    {
        public string Spotify { get; set; }
    }

    public class Followers
    {
        public object Href { get; set; }
        public int Total { get; set; }
    }
}
