using SpotifyRekommendations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRekommendations.Models
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
        public string Genre { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
    }
}
