using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyRecommendations.Entities
{
    public class SpotifyArtist : SpotifyItemBase
    {
        [JsonConstructor]
        public SpotifyArtist(IReadOnlyCollection<string> genres)
        {
            if(genres != null)
            {
                Genres = genres.Select(x => new SpotifyGenre { Name = x }).ToList();
            }
        }

        public int Popularity { get; set; }
        public Followers Followers { get; set; }
        public List<SpotifyGenre> Genres { get; set; }
        public List<SpotifyImage> Images { get; set; }

        [JsonProperty(PropertyName = "external_urls")]
        public ExternalUrls ExternalUrls { get; set; }
    }
}
