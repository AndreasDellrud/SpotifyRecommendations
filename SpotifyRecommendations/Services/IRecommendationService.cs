using SpotifyRecommendations.Entities;
using System.Collections.Generic;

namespace SpotifyRecommendations.Services
{
    public interface IRecommendationService
    {
        List<SpotifyTrack> GetRecommendations(List<SpotifyTrack> tracks, TrackFilter filter);
    }
}
