using SpotifyRecommendations.Entities;
using System.Collections.Generic;

namespace SpotifyRecommendations.Models
{
    public class TrackRecommendationViewModel
    {
        public SpotifyArtist Artist { get; set; }
        public TrackFilterViewModel TrackFilter { get; set; }
        public List<SpotifyTrack> Tracks { get; set; }
    }
}
