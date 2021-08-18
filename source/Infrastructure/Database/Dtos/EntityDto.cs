using System;
using LiteDB;

namespace Infrastructure.Database.Dtos
{
    public abstract class EntityDto
    {
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.Empty;

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

    }
}