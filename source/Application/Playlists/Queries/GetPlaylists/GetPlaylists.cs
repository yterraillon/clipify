using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Playlists.Queries.GetPlaylists
{
    public static class GetPlaylists
    {
        public record Request : IRequest<Response>;

        public record Response(IEnumerable<PlaylistViewModel> Playlists);

        public record PlaylistViewModel
        {
            public string Service { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string CoverImage { get; set; } = "about:blank";
            public DateTime LastUpdate { get; set; }
        };

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDataReader<Playlist> _playlistDataReader;

            public Handler(IDataReader<Playlist> playlistDataReader) => _playlistDataReader = playlistDataReader;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var playlists = _playlistDataReader.GetAll();
                var viewModels = ToViewModels(playlists);
                return Task.FromResult(new Response(viewModels));
            }

            private static IEnumerable<PlaylistViewModel> ToViewModels(IEnumerable<Playlist> playlists) =>
                playlists.Select(p => new PlaylistViewModel
                {
                    Service = p.Service,
                    Name = p.Name,
                    CoverImage = p.CoverImage,
                    LastUpdate = p.LastUpdate
                });
        }
    }
}