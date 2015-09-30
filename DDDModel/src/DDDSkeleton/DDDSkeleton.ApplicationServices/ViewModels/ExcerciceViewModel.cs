using System.Collections.Generic;

namespace DDDSkeleton.ApplicationServices.ViewModels
{
    public class ExcerciceViewModel
    {
        public string Name { get; set; }

        public int Number { get; set; }

        public List<EvaluationViewModel> EvaluationViewModels { get; set; }
    }
}