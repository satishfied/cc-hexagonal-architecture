namespace CircuitBreaker
{
    using System;

    internal class Program
    {
        #region Methods

        private static void Main(string[] args)
        {
            var breaker = new CircuitBreaker();

            for (int i = 0; i < 4; i++)
            {
                try
                {
                    breaker.ExecuteAction(() =>
                    {
                        // Operation protected by the circuit breaker.
                        //...
                        breaker.PerformIncredibaleAction(i);
                    });
                }
                catch (CircuitBreakerOpenException ex)
                {
                    // Perform some different action when the breaker is open.
                    // Last exception details are in the inner exception.
                    //...
                }
                catch (Exception ex)
                {
                    //...
                }
            }
        }

        #endregion
    }
}