using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyRecommendations.Entities;
using SpotifyRecommendations.Models;
using SpotifyRecommendations.Services;

namespace SpotifyRecommendations.Controllers
{
    public class ArtistController : Controller
    {
        private readonly ISpotifyApiService _spotifyApiService;

        public ArtistController(ISpotifyApiService spotifyApiService)
        {
            _spotifyApiService = spotifyApiService;
        }

        // GET: Artist/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var artist = await _spotifyApiService.GetSpotifyArtist(id);
            var relatedArtists = await _spotifyApiService.GetRelatedArtists(id);
            var topTracks = await _spotifyApiService.GetTopTracksForArtist(id);
            var backgroundImg = artist.Images.Where(x => x.Width == artist.Images.Max(y => y.Width)).FirstOrDefault();
            var viewModel = new ArtistDetailsViewModel
            {
                BackgroundImage = backgroundImg,
                Artist = artist,
                RelatedArtists = relatedArtists,
                TopTracks = topTracks.Take(5).ToList()
            };
            return View(viewModel);
        }


    }
}