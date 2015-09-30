using System;

namespace DDDSkeleton.ApplicationServices
{
    public class ServiceResponseBase
    {
        public ServiceResponseBase()
        {
            Exception = null;
        }

        public Exception Exception { get; set; }
    }
}