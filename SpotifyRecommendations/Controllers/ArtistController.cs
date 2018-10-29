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
        // GET: Artist
        public ActionResult Index()
        {
            var testId = "7jy3rLJdDQY21OgRLCZ9sD";
            var artist = _spotifyApiService.GetSpotifyArtist(testId);
            return View(artist);
        }

        // GET: Artist/Details/5
        public ActionResult Details(string id)
        {
            var artist = _spotifyApiService.GetSpotifyArtist(id).Result;
            var relatedArtists = _spotifyApiService.GetRelatedArtists(id).Result;
            var topTracks = _spotifyApiService.GetTopTracksForArtist(id).Result.Take(5);
            var backgroundImg = artist.Images.Where(x => x.Width == artist.Images.Max(y => y.Width)).FirstOrDefault();
            var viewModel = new ArtistDetailsViewModel
            {
                BackgroundImage = backgroundImg,
                Artist = artist,
                RelatedArtists = relatedArtists,
                TopTracks = topTracks.ToList()
            };
            return View(viewModel);
        }


    }
}