namespace DDDSkeleton.ApplicationServices
{
    public abstract class IntegerRequestBase : ServiceRequestBase
    {
        private readonly int _id;

        protected IntegerRequestBase(int id)
        {
            _id = id;
        }

        public int Id
        {
            get { return _id; }
        }
    }
}