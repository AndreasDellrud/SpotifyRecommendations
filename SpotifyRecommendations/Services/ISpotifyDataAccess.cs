using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Services
{
    public interface ISpotifyDataAccess
    {
        Task<string> GetResponseContent(HttpMethod method, string endpoint);
        Task<string> GetResponseContent(HttpMethod method, string endpoint, Dictionary<string, string> queryParameters);
        Task<string> GetResponseContent(HttpMethod method, string endpoint, Dictionary<string, string> queryParameters, bool isSearch);
    }
}
