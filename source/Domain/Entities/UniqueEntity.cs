namespace Domain.Entities
{
    public abstract class UniqueEntity : Entity
    {
        protected UniqueEntity(string defaultId) => Id = defaultId;
    }
}