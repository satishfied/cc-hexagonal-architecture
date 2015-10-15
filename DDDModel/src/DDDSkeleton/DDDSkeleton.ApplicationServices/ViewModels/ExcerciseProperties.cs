using System.Collections.Generic;

namespace DDDSkeleton.ApplicationServices.ViewModels
{
    public class ExcerciseProperties
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public List<EvaluationProperties> EvaluationProperties { get; set; }
    }
}