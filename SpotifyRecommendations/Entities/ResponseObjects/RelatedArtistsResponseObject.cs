using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Entities.ResponseObjects
{
    public class RelatedArtistsResponseObject
    {
        public List<SpotifyArtist> Artists { get; set; }
    }
}
