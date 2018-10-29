using SpotifyRecommendations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Models
{
    public class ArtistDetailsViewModel
    {
        public SpotifyImage BackgroundImage { get; set; }
        public SpotifyArtist Artist { get; set; }
        public List<SpotifyTrack> TopTracks { get; set; }
        public List<SpotifyArtist> RelatedArtists { get; set; }
    }
}
