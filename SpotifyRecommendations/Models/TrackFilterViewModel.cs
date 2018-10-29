﻿using SpotifyRecommendations.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Models
{
    public class TrackFilterViewModel
    {
        [Range(0.0, 1.0)]
        public double Danceability { get; set; } = 0.5;

        [Range(0.0, 1.0)]
        public double Energy { get; set; } = 0.5;

        [Range(0.0, 1.0)]
        public double Acousticness { get; set; } = 0.5;

        [Range(0.0, 1.0)]
        public double Instrumentalness { get; set; } = 0.5;

        [Range(0.0, 1.0)]
        public double Valence { get; set; } = 0.5;

        public bool IsSearchRequest { get; set; } = false;
    }
}
