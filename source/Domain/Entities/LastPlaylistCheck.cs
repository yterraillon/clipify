using System;

namespace Domain.Entities
{
    public class LastPlaylistCheck : UniqueEntity
    {
        public const string DefaultLastPlaylistCheckId = "default";

        public LastPlaylistCheck() : base(DefaultLastPlaylistCheckId)
        {
        }

        public DateTime DateTime { get; set; }
    }
}