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

            var audioFeatures = tracks.Select(x => x.AudioFeature).ToList();

            var bestMatches = FindBestMatch(audioFeatures, filter);

            var recommendations = new List<SpotifyTrack>();

            foreach (var track in bestMatches)
            {
                recommendations.AddRange(tracks.Where(x => x.Id == track.Id));
            }

            return recommendations;
        }

        private List<AudioProfile> FindBestMatch(List<SpotifyAudioFeature> audioFeatures, TrackFilter filter)
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

            return audioProfiles.OrderBy(x => x.Distance).Take(3).ToList();
        }

        private double GetDistance(SpotifyAudioFeature audioFeature, TrackFilter filter)
        {
            double danceabilityDiff;
            double energyDiff;
            double instrumentalnessDiff;
            double acousticnessDiff;
            double valenceDiff;

            danceabilityDiff = audioFeature.Danceability - filter.Danceability;
            energyDiff = audioFeature.Energy - filter.Energy;
            instrumentalnessDiff = audioFeature.Instrumentalness - filter.Instrumentalness;
            acousticnessDiff = audioFeature.Acousticness - filter.Acousticness;
            valenceDiff = audioFeature.Valence - filter.Valence;



            return Math.Sqrt(Math.Pow(danceabilityDiff, 2) + Math.Pow(energyDiff, 2) + Math.Pow(instrumentalnessDiff, 2) + Math.Pow(acousticnessDiff, 2) + Math.Pow(valenceDiff, 2));
        }

        private class AudioProfile
        {
            public double Distance { get; set; }
            public string Id { get; set; }
        }
    }
}
