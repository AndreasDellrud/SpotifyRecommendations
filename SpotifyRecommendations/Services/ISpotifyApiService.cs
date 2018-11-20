using SpotifyRecommendations.Entities;
using SpotifyRecommendations.Entities.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyRecommendations.Services
{
    public interface ISpotifyApiService
    {
        Task<List<SpotifyGenre>> GetAllGenres();
        Task<ResponseObject<SpotifyArtist>> SearchArtistByGenre(string genre, int limit, int offset);
        Task<SpotifyArtist> GetSpotifyArtist(string id);
        Task<List<SpotifyArtist>> GetRelatedArtists(string id);
        Task<List<SpotifyTrack>> GetTopTracksForArtist(string artistId);
        Task<List<SpotifyAudioFeature>> GetAudioFeatures(List<string> trackIds);
        Task<List<SpotifyAlbum>> GetSpotifyAlbumsForArtist(string artistId);
        Task<List<SpotifyTrack>> GetTracksByAlbumId(string albumId);
    }
}
