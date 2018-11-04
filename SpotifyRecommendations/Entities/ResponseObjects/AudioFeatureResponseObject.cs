using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Entities.ResponseObjects
{
    public class AudioFeatureResponseObject
    {
        [JsonProperty(PropertyName = "audio_features")]
        public List<SpotifyAudioFeature> AudioFeatures { get; set; }
    }
}
