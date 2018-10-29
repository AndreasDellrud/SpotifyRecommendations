using SpotifyRecommendations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Models
{
    public class SearchArtistViewModel
    {
        public SearchArtistViewModel()
        {
            Artists = new List<SpotifyArtist>();
            Limit = 5;
            Offset = 0;
            Genre = string.Empty;
            Total = 0;
        }

        public List<SpotifyArtist> Artists { get; set; }
        public List<SearchArtistResultIcon> Icons { get; set; }
        public string Genre { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
    }

    public class SearchArtistResultIcon
    {
        public SpotifyImage Icon { get; set; }
        public string ArtistId { get; set; }
    }
}
