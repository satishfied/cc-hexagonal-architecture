namespace CircuitBreaker
{
    using System;

    internal sealed class CircuitBreakerOpenException : Exception
    {
        public CircuitBreakerOpenException(Exception lastException)
        {
            throw new NotImplementedException();
        }
    }
}