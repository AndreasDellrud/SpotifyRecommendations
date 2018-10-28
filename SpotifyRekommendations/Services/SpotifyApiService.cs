using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotifyRekommendations.Entities;
using SpotifyRekommendations.Entities.ResponseObjects;
using SpotifyRekommendations.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyRekommendations.Services
{
    public class SpotifyApiService : ISpotifyApiService
    {
        private readonly ISpotifyDataAccess _spotifyDataAccess;

        public SpotifyApiService(ISpotifyDataAccess spotifyDataAccess)
        {
            _spotifyDataAccess = spotifyDataAccess;
        }

        public async Task<List<SpotifyGenre>> GetAllGenres()
        {
            var endpoint = "recommendations/available-genre-seeds";
            var getResponse = await _spotifyDataAccess.GetResponseContent(HttpMethod.Get, endpoint);
            var responseObject = JsonConvert.DeserializeObject<GenreResponseObject>(getResponse);

            var spotifyGenres = responseObject.Genres.Select(x => new SpotifyGenre() { Name = x.Capitalize() });

            return spotifyGenres.ToList();
        }

        public async Task<List<SpotifyCategory>> GetAllCategories()
        {

            var limit = 10;
            var offset = 0;
            int total = 0;
            var endpoint = "browse/categories";
            var queryParameters = new Dictionary<string, string>();
            var categories = new List<SpotifyCategory>();

            queryParameters.Add("offset", offset.ToString());
            queryParameters.Add("limit", limit.ToString());

            do
            {
                var getResponse = await _spotifyDataAccess.GetResponseContent(HttpMethod.Get, endpoint, queryParameters);
                var responseRoot = JsonConvert.DeserializeObject<CategoryRootObject>(getResponse);
                var responseObject = responseRoot.Categories;
                total = responseObject.Total;
                categories.AddRange(responseObject.Items);
                offset += responseObject.Limit;
                queryParameters["offset"] = offset.ToString();
            } while (categories.Count < total);

            return categories;
        }

        public async Task<ResponseObject<SpotifyArtist>> SearchArtistByGenre(string genre, int limit, int offset)
        {
            var endpoint = "search";
            var searchType = "artist";
            var queryParameters = new Dictionary<string, string>
            {
                { "q", $"genre:{genre}" },
                { "type", searchType },
                { "limit", limit.ToString() },
                { "offset", offset.ToString() }
            };

            var getResponse = await _spotifyDataAccess.GetResponseContent(HttpMethod.Get, endpoint, queryParameters);
            var responseRoot = JsonConvert.DeserializeObject<ArtistRootObject>(getResponse);
            var responseObject = responseRoot.Artists;

            return responseObject;
        }

        public async Task<SpotifyArtist> GetSpotifyArtist(string id)
        {
            var endpoint = $"artists/{id}";

            var getResponse = await _spotifyDataAccess.GetResponseContent(HttpMethod.Get, endpoint);
            var response = JsonConvert.DeserializeObject<SpotifyArtist>(getResponse);
            return response;
        }

        public async Task<List<SpotifyArtist>> GetRelatedArtists(string id)
        {
            var endpoint = $"artists/{id}/related-artists";

            var getResponse = await _spotifyDataAccess.GetResponseContent(HttpMethod.Get, endpoint);
            var response = JsonConvert.DeserializeObject<ResponseObject<SpotifyArtist>>(getResponse);
            var responseObject = response.Items;

            return responseObject;
        }
    }
}
