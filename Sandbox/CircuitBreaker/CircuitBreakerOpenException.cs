namespace CircuitBreaker
{
    using System;

    public class CircuitBreakerOpenException : Exception
    {
        public CircuitBreakerOpenException(Exception lastException)
            : base("The circuit breaker is in the open state.", lastException)
        {
        }
    }
}