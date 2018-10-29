using SpotifyRecommendations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyRecommendations.Services
{
    public class RecommendationService : IRecommendationService
    {
        public List<SpotifyTrack> GetRekommendations(List<SpotifyTrack> tracks, TrackFilter filter)
        {
            var perfectTracks = tracks.Where(x => x.AudioFeature.Danceability == filter.Danceability &&
                                                    x.AudioFeature.Energy == filter.Energy &&
                                                    x.AudioFeature.Instrumentalness == filter.Instrumentalness &&
                                                    x.AudioFeature.Acousticness == filter.Acousticness &&
                                                    x.AudioFeature.Valence == filter.Valence);
            if (perfectTracks.Any())
                return perfectTracks.ToList();

            var audioFeatures = tracks.Select(x => x.AudioFeature);

            var filterSum = (filter.Acousticness + filter.Danceability + filter.Energy + filter.Instrumentalness + filter.Valence);
            var filterMean = filterSum / 5;

            var audioProfiles = audioFeatures.Select(x => new AudioProfile
            {
                Sum = (x.Acousticness + x.Danceability + x.Energy + x.Instrumentalness + x.Valence),
                Mean = (x.Acousticness + x.Danceability + x.Energy + x.Instrumentalness + x.Valence) / 5,
                Id = x.Id
            });

            var closestSum = audioProfiles.OrderBy(x => Math.Abs(filterSum - x.Sum)).Take(3);
            var closestMean = audioProfiles.OrderBy(x => Math.Abs(filterMean - x.Mean)).Take(3);

            var recommendations = new List<SpotifyTrack>();

            foreach (var track in closestMean)
            {
                recommendations.AddRange(tracks.Where(x => x.Id == track.Id));
            }

            return recommendations;
        }

        private class AudioProfile
        {
            public double Sum { get; set; }
            public double Mean { get; set; }
            public string Id { get; set; }
        }
    }
}
