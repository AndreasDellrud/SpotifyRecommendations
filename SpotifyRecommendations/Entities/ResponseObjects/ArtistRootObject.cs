﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Entities.ResponseObjects
{
    public class ArtistRootObject
    {
        public ResponseObject<SpotifyArtist> Artists { get; set; }
    }
}
