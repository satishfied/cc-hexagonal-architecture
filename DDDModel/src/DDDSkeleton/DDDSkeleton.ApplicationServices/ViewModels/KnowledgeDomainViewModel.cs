using System.Collections.Generic;

namespace DDDSkeleton.ApplicationServices.ViewModels
{
    public class KnowledgeDomainViewModel
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public List<EvaluationViewModel> EvaluationViewModels { get; set; }
    }
}