namespace DDDSkeleton.Infrascructure.Common.Domain
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}