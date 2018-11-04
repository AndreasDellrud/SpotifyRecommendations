using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotifyRecommendations.Entities;
using SpotifyRecommendations.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Services
{
    public class SpotifyDataAccess : ISpotifyDataAccess
    {
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string spotifyAuthenticationUrl;
        private readonly string spotifyBaseUrl;

        public SpotifyDataAccess(IConfiguration configuration)
        {
            clientId = configuration["Spotify:ClientId"];
            clientSecret = configuration["Spotify:ClientSecret"];
            spotifyAuthenticationUrl = configuration["Spotify:AuthenticationUrl"];
            spotifyBaseUrl = configuration["Spotify:BaseUrl"];
        }

        public async Task<string> GetResponseContent(HttpMethod method, string endpoint)
        {
            var content = await GetRequestResponseString(HttpMethod.Get, endpoint);

            return content;
        }

        public async Task<string> GetResponseContent(HttpMethod method, string endpoint, Dictionary<string, string> queryParameters)
        {
            string url = QueryHelpers.AddQueryString(endpoint, queryParameters);

            var content = await GetRequestResponseString(HttpMethod.Get, url);

            return content;
        }

        public async Task<string> GetResponseContent(HttpMethod method, string endpoint, Dictionary<string, string> queryParameters, bool isSearch)
        {
            string url = QueryHelpers.AddQueryString(endpoint, queryParameters);

            var content = await GetRequestResponseString(HttpMethod.Get, url);

            return content;
        }

        private async Task<string> GetRequestResponseString(HttpMethod method, string requestUri)
        {
            try
            {
                var client = new HttpClient()
                {
                    BaseAddress = new Uri(spotifyBaseUrl)
                };

                var authenticationObject = await GetAuthenticationToken();

                var request = new HttpRequestMessage(method, requestUri);
                request.Headers.Add("ContentType", "application/json");
                request.Headers.Add("Authorization", $"Bearer {authenticationObject.AccessToken}");

                var response = await client.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Response: {result}");
                }

                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private async Task<SpotifyAuthenticationObject> GetAuthenticationToken()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(spotifyAuthenticationUrl)
            };

            var requestContent = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };

            var base64AuthString = $"{clientId}:{clientSecret}".ConvertToBase64();

            var request = new HttpRequestMessage(HttpMethod.Post, "token")
            {
                Content = new FormUrlEncodedContent(requestContent)
            };
            request.Headers.Add("ContentType", "application/x-www-form-urlencoded");
            request.Headers.Add("Authorization", $"Basic {base64AuthString}");

            var response = await client.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Response: {result}");
            }

            return JsonConvert.DeserializeObject<SpotifyAuthenticationObject>(result);
        }
    }
}
