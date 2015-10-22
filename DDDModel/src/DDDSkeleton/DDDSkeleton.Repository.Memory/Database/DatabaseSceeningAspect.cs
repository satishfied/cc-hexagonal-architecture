namespace DDDSkeleton.Repository.Memory.Database
{
    public class DatabaseSceeningAspect
    {
        public enum AspectTypes
        {
            Excercise,
            KnwoledgeDomain
        }

        public string Name { get; set; }
        public int AspectType { get; set; }
        public string Remark { get; set; }
        public int Score { get; set; }
    }
}