using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Entities.ResponseObjects
{
    public class AlbumRootObject
    {
        public ResponseObject<SpotifyAlbum> Items { get; set; }
    }
}
