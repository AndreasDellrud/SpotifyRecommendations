using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
