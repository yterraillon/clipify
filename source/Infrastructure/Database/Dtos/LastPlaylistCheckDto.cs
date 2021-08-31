using System;
using Domain.Entities;

namespace Infrastructure.Database.Dtos
{
    public class LastPlaylistCheckDto : EntityDto
    {
        public LastPlaylistCheckDto() => Id = LastPlaylistCheck.DefaultLastPlaylistCheckId;

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}