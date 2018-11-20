using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyRecommendations.Entities
{
    public class SpotifyArtist
    {
        [JsonConstructor]
        public SpotifyArtist(IReadOnlyCollection<string> genres)
        {
            if(genres != null)
            {
                Genres = genres.Select(x => new SpotifyGenre { Name = x }).ToList();
            }
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
        public int Popularity { get; set; }
        public Followers Followers { get; set; }
        public List<SpotifyGenre> Genres { get; set; }
        public List<SpotifyImage> Images { get; set; }

        [JsonProperty(PropertyName = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }
    }
}
