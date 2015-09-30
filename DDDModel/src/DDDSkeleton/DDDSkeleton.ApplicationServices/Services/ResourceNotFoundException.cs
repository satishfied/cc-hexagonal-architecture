using System;

namespace DDDSkeleton.ApplicationServices.Services
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException():base("The requested resource was not found!")
        {
        }

        public ResourceNotFoundException(string message) : base(message)
        {
        }
    }
}