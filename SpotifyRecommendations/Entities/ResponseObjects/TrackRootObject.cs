using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Entities.ResponseObjects
{
    public class TrackRootObject
    {
        public ResponseObject<SpotifyTrack> Tracks { get; set; }
    }
}
