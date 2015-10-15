using System.Collections.Generic;

namespace DDDSkeleton.ApplicationServices.ViewModels
{
    public class KnowledgeDomainProperties
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public List<EvaluationProperties> EvaluationProperties { get; set; }
    }
}