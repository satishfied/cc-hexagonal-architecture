using System.Collections.Generic;
using DDDSkeleton.ApplicationServices.ViewModels;

namespace DDDSkeleton.ApplicationServices.Screenings
{
    public class GetScreeningsResponse : ServiceResponseBase
    {
        public IEnumerable<ScreeningViewModel> Screenings { get; set; }
    }
}