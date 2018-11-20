using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpotifyRecommendations.Entities.ResponseObjects
{
    public class AudioFeatureResponseObject
    {
        [JsonProperty(PropertyName = "audio_features")]
        public List<SpotifyAudioFeature> AudioFeatures { get; set; }
    }
}
