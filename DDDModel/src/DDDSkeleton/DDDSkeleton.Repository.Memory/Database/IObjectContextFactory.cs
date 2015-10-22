namespace DDDSkeleton.Repository.Memory.Database
{
    public interface IObjectContextFactory
    {
        InMemoryDatabaseObjectContext Create();
    }
}