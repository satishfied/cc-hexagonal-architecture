namespace CircuitBreaker
{
    using System;

    public class CircuitBreakerStateStoreFactory
    {
        private static ICircuitBreakerStateStore circuitBreakerStateStore;
        private static object sema = new object();

        public static ICircuitBreakerStateStore GetCircuitBreakerStateStore()
        {
            if (circuitBreakerStateStore == null)
            {
                lock (sema)
                {
                    if (circuitBreakerStateStore == null)
                    {
                        circuitBreakerStateStore = new InMemoryCircuitBreakerStateStore();
                    }
                }
            }

            return circuitBreakerStateStore;
        }
    }
}