using Microsoft.AspNetCore.Mvc;
using SpotifyRecommendations.Entities;
using SpotifyRecommendations.Models;
using SpotifyRecommendations.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SpotifyRecommendations.Controllers
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
                searchArtistViewModel.Icons = new List<SearchArtistResultIcon>();
                foreach (var artist in artistResponseObject.Items)
                {
                    var icon = artist.Images.Where(x => x.Width == artist.Images.Max(y => y.Width)).FirstOrDefault();
                    if (icon != null)
                    {
                        searchArtistViewModel.Icons.Add(new SearchArtistResultIcon
                        {
                            ArtistId = artist.Id,
                            Icon = icon,
                        });
                    }
                    else
                    {
                        searchArtistViewModel.Icons.Add(new SearchArtistResultIcon
                        {
                            ArtistId = artist.Id,
                            Icon = new SpotifyImage
                            {
                                Height = 0,
                                Width = 0,
                                Url = "/images/no-image.png"
                            },
                        });
                    }
                }
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
