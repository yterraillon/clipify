using System;
using LiteDB;

namespace Infrastructure.Database.Dtos
{
    public abstract class EntityDto
    {
        [BsonId]
        public string Id { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

    }
}