using System;

namespace Clipify.Domain.Common
{
    public abstract class Entity
    {
        public string Id { get; set; } = String.Empty;

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public string CreatedBy { get; set; } = String.Empty;

        public string UpdatedBy { get; set; } = String.Empty;
    }
}