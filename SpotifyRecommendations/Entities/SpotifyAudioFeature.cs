using Newtonsoft.Json;

namespace SpotifyRecommendations.Entities
{
    public class SpotifyAudioFeature
    {
        public double Danceability { get; set; }
        public double Energy { get; set; }
        public int Key { get; set; }
        public double Loudness { get; set; }
        public int Mode { get; set; }
        public double Speechiness { get; set; }
        public double Acousticness { get; set; }
        public double Instrumentalness { get; set; }
        public double Liveness { get; set; }
        public double Valence { get; set; }
        public double Tempo { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }
        public string Uri { get; set; }

        [JsonProperty(PropertyName = "track_href")]
        public string TrackHref { get; set; }

        [JsonProperty(PropertyName = "analysis_url")]
        public string AnalysisUrl { get; set; }

        [JsonProperty(PropertyName = "duration_ms")]
        public int DurationMs { get; set; }

        [JsonProperty(PropertyName = "time_signature")]
        public int TimeSignature { get; set; }
    }
}
