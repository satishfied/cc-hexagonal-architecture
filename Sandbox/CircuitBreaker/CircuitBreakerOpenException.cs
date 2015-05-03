namespace CircuitBreaker
{
    using System;

    internal sealed class CircuitBreakerOpenException : Exception
    {
        public CircuitBreakerOpenException(Exception lastException)
            : base("The circuit breaker is in the open state.", lastException)
        {
        }
    }
}