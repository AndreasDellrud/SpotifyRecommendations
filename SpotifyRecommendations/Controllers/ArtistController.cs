using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SpotifyRecommendations.Models;
using SpotifyRecommendations.Services;

namespace SpotifyRecommendations.Controllers
{
    public class ArtistController : Controller
    {
        private readonly ISpotifyApiService _spotifyApiService;
        private readonly IConfiguration _configuration;
        private const int FallbackNumberOfTracks = 5;

        public ArtistController(ISpotifyApiService spotifyApiService, IConfiguration configuration)
        {
            _spotifyApiService = spotifyApiService;
            _configuration = configuration;
        }

        // GET: Artist/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var numberOfTopTracks = int.TryParse(_configuration["Options:NumberOfTopTracks"], out var result) ? result : FallbackNumberOfTracks;

            var artist = await _spotifyApiService.GetSpotifyArtist(id);
            var relatedArtists = await _spotifyApiService.GetRelatedArtists(id);
            var topTracks = await _spotifyApiService.GetTopTracksForArtist(id);
            var backgroundImg = artist.Images.FirstOrDefault(x => x.Width == artist.Images.Max(y => y.Width));
            var viewModel = new ArtistDetailsViewModel
            {
                BackgroundImage = backgroundImg,
                Artist = artist,
                RelatedArtists = relatedArtists,
                TopTracks = topTracks.Take(numberOfTopTracks).ToList()
            };
            return View(viewModel);
        }


    }
}