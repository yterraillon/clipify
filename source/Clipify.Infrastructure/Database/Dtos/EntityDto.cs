using System;
using LiteDB;

namespace Clipify.Infrastructure.Database.Dtos
{
    public class EntityDto
    {
        public ObjectId Id { get; set; } = ObjectId.Empty;

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public string UpdatedBy { get; set; } = string.Empty;
    }
}