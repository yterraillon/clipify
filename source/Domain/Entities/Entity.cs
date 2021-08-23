using System;

namespace Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; } = Guid.Empty;

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }
    }
}