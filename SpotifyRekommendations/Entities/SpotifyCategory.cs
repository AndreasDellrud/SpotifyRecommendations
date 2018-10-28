﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRekommendations.Entities
{
    public class SpotifyCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public List<SpotifyImage> Icons { get; set; }
    }
}
