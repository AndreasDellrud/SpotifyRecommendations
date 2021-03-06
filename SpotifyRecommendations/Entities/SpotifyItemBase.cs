﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Entities
{
    public class SpotifyItemBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
    }
}
