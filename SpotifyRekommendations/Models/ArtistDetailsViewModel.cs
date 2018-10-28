using SpotifyRekommendations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRekommendations.Models
{
    public class ArtistDetailsViewModel
    {
        public SpotifyArtist Artist { get; set; }
        public List<SpotifyArtist> RelatedArtists { get; set; }
    }
}
