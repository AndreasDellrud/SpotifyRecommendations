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
        private readonly ISpotifyApiService _spotifyApiSerice;
        private readonly IRecommendationService _recommendationService;

        public TrackController(ISpotifyApiService spotifyApiService, IRecommendationService recommendationService)
        {
            _spotifyApiSerice = spotifyApiService;
            _recommendationService = recommendationService;
        }
        public IActionResult Index(string id, TrackFilterViewModel trackFilter)
        {
            var artist = _spotifyApiSerice.GetSpotifyArtist(id).Result;
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
                var albums = _spotifyApiSerice.GetSpotifyAlbumsForArtist(id).Result;

                foreach(var album in albums)
                {
                    var albumTracks = _spotifyApiSerice.GetTracksByAlbumId(album.Id).Result;
                    foreach(var track in albumTracks)
                    {
                        track.Album = album;
                        track.AudioFeature = _spotifyApiSerice.GetAudioFeatures(track.Id).Result;
                        tracks.Add(track);
                    }
                }
                recommendations = _recommendationService.GetRekommendations(tracks, filter);
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