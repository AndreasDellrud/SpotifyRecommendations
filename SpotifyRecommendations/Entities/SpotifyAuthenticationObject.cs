﻿using System;

namespace SpotifyRecommendations.Entities
{
    public class SpotifyAuthenticationObject
    {
        public SpotifyAuthenticationObject(string access_token, int expires_in)
        {
            AccessToken = access_token;
            ExpiresInSeconds = expires_in;
        }

        public string AccessToken { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public int ExpiresInSeconds { get; set; }
    }
}
