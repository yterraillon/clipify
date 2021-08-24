using LiteDB;
using System;

namespace Clipify.Infrastructure.Database.Dtos
{
    public abstract class EntityDto
    {
        [BsonId]
        public string Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public string UpdatedBy { get; set; } = string.Empty;
    }
}