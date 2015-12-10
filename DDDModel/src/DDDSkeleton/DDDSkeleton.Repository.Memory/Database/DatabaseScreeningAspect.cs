namespace DDDSkeleton.Repository.Memory.Database
{
    public class DatabaseScreeningAspect
    {
        public enum AspectTypes
        {
            Excercise,
            KnowledgeDomain
        }

        public string Name { get; set; }
        public int AspectType { get; set; }
        public string Remark { get; set; }
        public int Score { get; set; }
    }
}