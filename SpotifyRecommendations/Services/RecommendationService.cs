using SpotifyRecommendations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyRecommendations.Services
{
    public class RecommendationService : IRecommendationService
    {
        public List<SpotifyTrack> GetRecommendations(List<SpotifyTrack> tracks, TrackFilter filter, int recommendationCount)
        {
            var perfectTracks = tracks.Where(x => x.AudioFeature.Danceability == filter.Danceability &&
                                                    x.AudioFeature.Energy == filter.Energy &&
                                                    x.AudioFeature.Instrumentalness == filter.Instrumentalness &&
                                                    x.AudioFeature.Acousticness == filter.Acousticness &&
                                                    x.AudioFeature.Valence == filter.Valence);

            var spotifyTracks = perfectTracks.ToList();
            if (spotifyTracks.Any())
                return spotifyTracks.ToList();

            var audioFeatures = tracks.Select(x => x.AudioFeature).ToList();

            var bestMatches = FindBestMatch(audioFeatures, filter, recommendationCount);

            var recommendations = new List<SpotifyTrack>();

            foreach (var track in bestMatches)
            {
                recommendations.AddRange(tracks.Where(x => x.Id == track.Id));
            }

            return recommendations;
        }

        private IEnumerable<AudioProfile> FindBestMatch(List<SpotifyAudioFeature> audioFeatures, TrackFilter filter, int recommendationCount)
        {
            var audioProfiles = new List<AudioProfile>();

            foreach (var feature in audioFeatures)
            {
                audioProfiles.Add(new AudioProfile
                {
                    Id = feature.Id,
                    Distance = GetDistance(feature, filter)
                });
            }

            return audioProfiles.OrderBy(x => x.Distance).Take(recommendationCount).ToList();
        }

        private static double GetDistance(SpotifyAudioFeature audioFeature, TrackFilter filter)
        {
            var danceabilityDiff = audioFeature.Danceability - filter.Danceability;
            var energyDiff = audioFeature.Energy - filter.Energy;
            var instrumentalityDiff = audioFeature.Instrumentalness - filter.Instrumentalness;
            var acousticnessDiff = audioFeature.Acousticness - filter.Acousticness;
            var valenceDiff = audioFeature.Valence - filter.Valence;

            return Math.Sqrt(Math.Pow(danceabilityDiff, 2) + Math.Pow(energyDiff, 2) + Math.Pow(instrumentalityDiff, 2) + Math.Pow(acousticnessDiff, 2) + Math.Pow(valenceDiff, 2));
        }

        private class AudioProfile
        {
            public double Distance { get; set; }
            public string Id { get; set; }
        }
    }
}
