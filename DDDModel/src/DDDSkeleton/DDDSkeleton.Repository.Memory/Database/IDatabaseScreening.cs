using System;
using System.Collections.Generic;

namespace DDDSkeleton.Repository.Memory.Database
{
    public interface IDatabaseScreening
    {
        int Id { get; set; }
        string Candidate { get; set; }
        string Recruiter { get; set; }
        DateTime Date { get; set; }
        string Location { get; set; }
        string Remark { get; set; }
        string GlobalEvaluation { get; set; }
        List<DatabaseScreeningAspect> Aspects { get; set; }
    }
}