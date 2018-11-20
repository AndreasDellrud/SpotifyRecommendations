using SpotifyRecommendations.Entities;
using System.Collections.Generic;

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
