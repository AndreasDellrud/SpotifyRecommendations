using System.Collections.Generic;

namespace SpotifyRecommendations.Entities.ResponseObjects
{
    public class ResponseObject<T>
    {
        public List<T> Items { get; set; }
        public string Href { get; set; }
        public int Limit { get; set; }
        public string Next { get; set; }
        public int Offset { get; set; }
        public object Previous { get; set; }
        public int Total { get; set; }
    }
}
