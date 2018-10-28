using SpotifyRekommendations.Entities;
using SpotifyRekommendations.Entities.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRekommendations.Services
{
    public interface ISpotifyApiService
    {
        Task<List<SpotifyGenre>> GetAllGenres();
        Task<List<SpotifyCategory>> GetAllCategories();
        Task<ResponseObject<SpotifyArtist>> SearchArtistByGenre(string genre, int limit, int offset);
        Task<SpotifyArtist> GetSpotifyArtist(string id);
        Task<List<SpotifyArtist>> GetRelatedArtists(string id);
    }
}
