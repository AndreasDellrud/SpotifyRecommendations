using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpotifyRecommendations.Entities;
using SpotifyRecommendations.Models;
using SpotifyRecommendations.Services;

namespace SpotifyRecommendations.Controllers
{
    public class TrackController : Controller
    {
        private readonly ISpotifyApiService _spotifyApiService;
        private readonly IRecommendationService _recommendationService;

        public TrackController(ISpotifyApiService spotifyApiService, IRecommendationService recommendationService)
        {
            _spotifyApiService = spotifyApiService;
            _recommendationService = recommendationService;
        }
        public async Task<IActionResult> Index(string id, TrackFilterViewModel trackFilter)
        {
            var artist = await _spotifyApiService.GetSpotifyArtist(id);
            var tracks = new List<SpotifyTrack>();
            var recommendations = new List<SpotifyTrack>();
            if(trackFilter.IsSearchRequest)
            {
                var filter = new TrackFilter
                {
                    Danceability = trackFilter.Danceability,
                    Energy = trackFilter.Energy,
                    Acousticness = trackFilter.Acousticness,
                    Instrumentalness = trackFilter.Instrumentalness,
                    Valence = trackFilter.Valence
                };
                var albums = await _spotifyApiService.GetSpotifyAlbumsForArtist(id);

                var albumTracksLists = await Task.WhenAll(albums.Select(x => _spotifyApiService.GetTracksByAlbumId(x.Id)));

                albumTracksLists.ToList().ForEach(x => x.ForEach(y =>
                {
                    y.Album = albums.First(z => z.Id == y.AlbumId);
                    tracks.Add(y);
                }));

                recommendations = _recommendationService.GetRecommendations(tracks, filter);
            }
            trackFilter.IsSearchRequest = true;
            var viewModel = new TrackRecommendationViewModel()
            {
                Artist = artist,
                TrackFilter = trackFilter,
                Tracks = recommendations
            };

            return View(viewModel);
        }
    }
}