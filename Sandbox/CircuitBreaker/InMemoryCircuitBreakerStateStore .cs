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
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void HalfOpen()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
