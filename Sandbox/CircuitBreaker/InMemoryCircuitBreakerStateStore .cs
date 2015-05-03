using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitBreaker
{
    class InMemoryCircuitBreakerStateStore : ICircuitBreakerStateStore
    {
        public CircuitBreakerStateEnum State
        {
            get;
            private set;
        }

        public Exception LastException
        {
            get;
            private set;
        }

        public DateTime LastStateChangedDateUtc
        {
            get;
            private set;
        }

        public void Trip(Exception ex)
        {
            this.State = CircuitBreakerStateEnum.Open;
            this.LastException = ex;
        }

        public void Reset()
        {
            this.State = CircuitBreakerStateEnum.Closed;
        }

        public void HalfOpen()
        {
            this.State = CircuitBreakerStateEnum.HalfOpen;
        }

        public bool IsClosed
        {
            get
            {
                return this.State == CircuitBreakerStateEnum.Closed;
            }
        }
    }
}
