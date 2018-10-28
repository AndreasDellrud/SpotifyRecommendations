using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyRekommendations.Entities;
using SpotifyRekommendations.Models;
using SpotifyRekommendations.Services;

namespace SpotifyRekommendations.Controllers
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
            return View();
        }

        // GET: Artist/Details/5
        public ActionResult Details(string id)
        {
            var artist = _spotifyApiService.GetSpotifyArtist(id).Result;
            var relatedArtists = _spotifyApiService.GetRelatedArtists(id).Result;

            var viewModel = new ArtistDetailsViewModel
            {
                Artist = artist,
                RelatedArtists = relatedArtists
            };
            return View(viewModel);
        }


    }
}