using System;
using System.Collections.Generic;

namespace DDDSkeleton.ApplicationServices.ViewModels
{
    public class ScreeningProperties
    {
        public string Candidate { get; set; }
        public string Recruiter { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Remark { get; set; }
        public string GlobalEvaluation { get; set; }
        public List<ExcerciseProperties> ExcerciseProperties { get; set; }
        public List<KnowledgeDomainProperties> KnowledgeDomainProperties { get; set; }
    }
}