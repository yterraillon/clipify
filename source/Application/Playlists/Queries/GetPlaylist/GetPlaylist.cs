using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain;
using Domain.Entities;
using MediatR;

namespace Application.Playlists.Queries.GetPlaylist
{
    public static class GetPlaylist
    {
        public record Request(string PlaylistId) : IRequest<Response>;

        public record Response(PlaylistViewModel Playlist);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepository<Playlist> _playlistRepository;
            private readonly ISpotifyPlaylistService _spotifyPlaylistService;

            public Handler(IRepository<Playlist> playlistRepository, ISpotifyPlaylistService spotifyPlaylistService)
            {
                _playlistRepository = playlistRepository;
                _spotifyPlaylistService = spotifyPlaylistService;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var localPlaylist = _playlistRepository.Get(request.PlaylistId);
                var playlist = await GetPlaylistFromService(localPlaylist.Id, localPlaylist.Service, cancellationToken);
                return new Response(playlist);
            }

            private async Task<PlaylistViewModel> GetPlaylistFromService(string playlistId, string service, CancellationToken cancellationToken)
            {
                if (service.Equals(Constants.Services.Spotify))
                {
                    return await _spotifyPlaylistService.GetPlaylist(playlistId, cancellationToken);
                }

                throw new Exception("Invalid Service Name");
            }
        }
    }
}