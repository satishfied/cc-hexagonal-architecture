using System;
using System.Collections.Generic;

namespace DDDSkeleton.Repository.Memory.Database
{
    public class DatabaseScreening
    {
        public int Id { get; set; }
        public string Candidate { get; set; }
        public string Recruiter { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Remark { get; set; }
        public string GlobalEvaluation { get; set; }
        public List<DatabaseSceeningAspect> Aspects { get; set; }
    }
}