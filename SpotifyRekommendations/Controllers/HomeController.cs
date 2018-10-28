using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotifyRekommendations.Entities;
using SpotifyRekommendations.Models;
using SpotifyRekommendations.Services;

namespace SpotifyRekommendations.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISpotifyApiService _spotifyApiService;

        public HomeController(ISpotifyApiService spotifyApiService)
        {
            _spotifyApiService = spotifyApiService;
        }

        public IActionResult Index()
        {
            ViewData["Genres"] = _spotifyApiService.GetAllGenres().Result.Select(x => x.Name).ToList();
            return View();
        }

        public IActionResult Search(string genre, int limit, int offset)
        {
            var newLimit = limit == 0 ? 5 : limit;

            var searchArtistViewModel = new SearchArtistViewModel
            {
                Artists = new List<SpotifyArtist>(),
                Limit = newLimit,
                Offset = 0,
                Genre = genre
            };
            if (!string.IsNullOrEmpty(genre))
            {
                var artistResponseObject = _spotifyApiService.SearchArtistByGenre(genre, newLimit, offset).Result;
                searchArtistViewModel.Artists = artistResponseObject.Items;
                searchArtistViewModel.Total = artistResponseObject.Total;
                searchArtistViewModel.Offset = artistResponseObject.Offset;
                searchArtistViewModel.Limit = artistResponseObject.Limit;
            }
            return PartialView("_ArtistResult", searchArtistViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
