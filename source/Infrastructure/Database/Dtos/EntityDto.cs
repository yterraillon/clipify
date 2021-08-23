using System;
using LiteDB;

namespace Infrastructure.Database.Dtos
{
    public abstract class EntityDto
    {
        [BsonId]
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

    }
}