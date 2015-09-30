using System.Collections.Generic;
using DDDSkeleton.Domain;

namespace DDDSkeleton.ApplicationServices.Screenings
{
    public class GetScreeningsResponse : ServiceResponseBase
    {
        public IEnumerable<Screening> Screenings { get; set; }
    }
}