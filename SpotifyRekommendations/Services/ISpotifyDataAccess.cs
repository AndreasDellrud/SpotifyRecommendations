using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyRekommendations.Services
{
    public interface ISpotifyDataAccess
    {
        Task<string> GetResponseContent(HttpMethod method, string endpoint);
        Task<string> GetResponseContent(HttpMethod method, string endpoint, Dictionary<string, string> queryParameters);
        Task<string> GetResponseContent(HttpMethod method, string endpoint, Dictionary<string, string> queryParameters, bool isSearch);
        Task<string> GetResponseContent(string url);
    }
}
