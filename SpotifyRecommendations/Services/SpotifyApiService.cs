using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotifyRecommendations.Entities;
using SpotifyRecommendations.Entities.ResponseObjects;
using SpotifyRecommendations.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Services
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
            var response = JsonConvert.DeserializeObject<RelatedArtistsResponseObject>(getResponse);
            var responseObject = response.Artists;

            return responseObject;
        }

        public async Task<List<SpotifyTrack>> GetTopTracksForArtist(string artistId)
        {
            var endpoint = $"artists/{artistId}/top-tracks";
            var queryParameters = new Dictionary<string, string>
            {
                { "country", "SE" }
            };

            var getResponse = await _spotifyDataAccess.GetResponseContent(HttpMethod.Get, endpoint, queryParameters);
            var response = JsonConvert.DeserializeObject<TrackResponseObject>(getResponse);
            var responseObject = response.Tracks;

            foreach (var track in responseObject)
            {
                track.AudioFeature = GetAudioFeatures(track.Id).Result;
            }

            return responseObject;
        }

        public async Task<SpotifyAudioFeature> GetAudioFeatures(string trackId)
        {
            var endpoint = $"audio-features/{trackId}";

            var getResponse = await _spotifyDataAccess.GetResponseContent(HttpMethod.Get, endpoint);
            var response = JsonConvert.DeserializeObject<SpotifyAudioFeature>(getResponse);

            return response;
        }

        public async Task<List<SpotifyAlbum>> GetSpotifyAlbumsForArtist(string artistId)
        {
            var limit = 50;
            var offset = 0;
            int total = 0;
            var endpoint = $"artists/{artistId}/albums";
            var queryParameters = new Dictionary<string, string>();
            var albums = new List<SpotifyAlbum>();

            queryParameters.Add("include_groups", "album");
            queryParameters.Add("market", "SE");
            queryParameters.Add("offset", offset.ToString());
            queryParameters.Add("limit", limit.ToString());

            do
            {
                var getResponse = await _spotifyDataAccess.GetResponseContent(HttpMethod.Get, endpoint, queryParameters);
                var responseObject = JsonConvert.DeserializeObject<ResponseObject<SpotifyAlbum>>(getResponse);
                total = responseObject.Total;
                albums.AddRange(responseObject.Items);
                offset += responseObject.Limit;
                queryParameters["offset"] = offset.ToString();
            } while (albums.Count < total);

            return albums;
        }

        public async Task<List<SpotifyTrack>> GetTracksByAlbumId(string albumId)
        {
            var limit = 50;
            var offset = 0;
            int total = 0;
            var endpoint = $"albums/{albumId}/tracks";
            var queryParameters = new Dictionary<string, string>();
            var tracks = new List<SpotifyTrack>();

            queryParameters.Add("offset", offset.ToString());
            queryParameters.Add("limit", limit.ToString());

            do
            {
                var getResponse = await _spotifyDataAccess.GetResponseContent(HttpMethod.Get, endpoint, queryParameters);
                var responseObject = JsonConvert.DeserializeObject<ResponseObject<SpotifyTrack>>(getResponse);
                total = responseObject.Total;
                tracks.AddRange(responseObject.Items);
                offset += responseObject.Limit;
                queryParameters["offset"] = offset.ToString();
            } while (tracks.Count < total);
            return tracks;
        }
    }
}
