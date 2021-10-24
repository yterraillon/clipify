using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Playlists.Queries.GetLocalPlaylists
{
    public static class GetLocalPlaylists
    {
        public record Request : IRequest<Response>;

        public record Response(IEnumerable<PlaylistViewModel> Playlists);

        public record PlaylistViewModel
        {
            public string Id { get; set; } = string.Empty;
            public string Service { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string CoverImage { get; set; } = "about:blank";
            public DateTime LastUpdate { get; set; }
            public bool IsAClone { get; set; }
        };

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDataReader<PlaylistViewModel> _playlistDataReader;

            public Handler(IDataReader<PlaylistViewModel> playlistDataReader) => _playlistDataReader = playlistDataReader;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var playlistViewModels = _playlistDataReader.GetAll();
                return Task.FromResult(new Response(playlistViewModels));
            }

            //private static IEnumerable<PlaylistViewModel> ToViewModels(IEnumerable<Playlist> playlists) =>
            //    playlists.Select(p => new PlaylistViewModel
            //    {
            //        Id = p.Id,
            //        Service = p.Service,
            //        Name = p.Name,
            //        CoverImage = p.CoverImage,
            //        LastUpdate = p.LastUpdate,
            //        IsAClone = p.IsAClone
            //    });
        }
    }
}