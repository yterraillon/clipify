using Domain.Entities;

namespace Domain
{
    public abstract class UniqueEntity : Entity
    {
        protected UniqueEntity(string defaultId) => Id = defaultId;
    }
}