using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotifyRekommendations.Entities;
using SpotifyRekommendations.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyRekommendations.Services
{
    public class SpotifyDataAccess : ISpotifyDataAccess
    {
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string spotifyAuthenticationUrl;
        private readonly string spotifyBaseUrl;
        private SpotifyAuthenticationObject authenticationObject;

        public SpotifyDataAccess(IConfiguration configuration)
        {
            clientId = configuration["Spotify:ClientId"];
            clientSecret = configuration["Spotify:ClientSecret"];
            spotifyAuthenticationUrl = configuration["Spotify:AuthenticationUrl"];
            spotifyBaseUrl = configuration["Spotify:BaseUrl"];

            if (authenticationObject == null)
            {
                authenticationObject = GetAuthenticationToken().Result;
            }
        }

        public async Task<string> GetResponseContent(HttpMethod method, string endpoint)
        {
            try
            {
                if (authenticationObject.TimeStamp.AddSeconds(authenticationObject.ExpiresInSeconds) > DateTime.Now)
                {
                    authenticationObject = GetAuthenticationToken().Result;
                }

                var client = new HttpClient()
                {
                    BaseAddress = new Uri(spotifyBaseUrl)
                };

                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
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
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> GetResponseContent(string url)
        {
            try
            {
                if (authenticationObject.TimeStamp.AddSeconds(authenticationObject.ExpiresInSeconds) > DateTime.Now)
                {
                    authenticationObject = GetAuthenticationToken().Result;
                }

                var uri = new Uri(url);


                var client = new HttpClient()
                {
                    BaseAddress = new Uri(uri.AbsolutePath)
                };

                var request = new HttpRequestMessage(HttpMethod.Get, uri.Query);
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
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> GetResponseContent(HttpMethod method, string endpoint, Dictionary<string, string> queryParameters)
        {
            try
            {
                if (authenticationObject.TimeStamp.AddSeconds(authenticationObject.ExpiresInSeconds) > DateTime.Now)
                {
                    authenticationObject = GetAuthenticationToken().Result;
                }

                var client = new HttpClient()
                {
                    BaseAddress = new Uri(spotifyBaseUrl)
                };

                string url = QueryHelpers.AddQueryString(endpoint, queryParameters);

                var request = new HttpRequestMessage(HttpMethod.Get, url);
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
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> GetResponseContent(HttpMethod method, string endpoint, Dictionary<string, string> queryParameters, bool isSearch)
        {
            try
            {
                if (authenticationObject.TimeStamp.AddSeconds(authenticationObject.ExpiresInSeconds) > DateTime.Now)
                {
                    authenticationObject = GetAuthenticationToken().Result;
                }

                var client = new HttpClient()
                {
                    BaseAddress = new Uri(spotifyBaseUrl)
                };

                string url = QueryHelpers.AddQueryString(endpoint, queryParameters);

                var request = new HttpRequestMessage(HttpMethod.Get, url);
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
            catch (Exception e)
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
