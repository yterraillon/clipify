using System;

namespace Domain.Entities
{
    public abstract class Entity
    {
        public string Id { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }
    }
}