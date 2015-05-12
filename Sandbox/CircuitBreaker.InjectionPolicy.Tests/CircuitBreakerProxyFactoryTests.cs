using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircuitBreaker.InjectionPolicy.Tests
{
    [TestClass]
    public class CircuitBreakerProxyFactoryTests
    {
        [TestMethod]
        public void CircuitBreakerProxyFactoryTests_RegularCall()
        {
           Assert.AreEqual(10,RunTargetCall().WorkDoneCount);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),AllowDerivedTypes=true)]
        public void CircuitBreakerProxyFactoryTests_ExceptionOnCall()
        {
            RunFailingTargetCall();
        }

        [TestMethod]
        [ExpectedException(typeof(CircuitBreakerOpenException), AllowDerivedTypes = true)]
        public void CircuitBreakerProxyFactoryTests_CallNotArrived_CircuitDown()
        {
 
            try
            {
                RunFailingTargetCall();
            }
            catch (Exception)
            {
                //now the circuit is closed and should return CircuitBreakerOpenException on second action ..
                RunTargetCall();
            }
        }

        [TestMethod] 
        public void CircuitBreakerProxyFactoryTests_CallNotArrived_CircuitDown_ThenBackOpen()
        {

            CircuitBreakerProxyFactory<MockCircuitBreakerTarget>
                    .Settings.HalfOpenWaitTime = new TimeSpan(0, 0, 5);
             
            try
            {
                RunFailingTargetCall();
            }
            catch (Exception ex)
            {
                //now the circuit is closed and should return CircuitBreakerOpenException on second action ..
                //unless we wait > 5 sec.
                //and we suspend breaking mode on Mock

                System.Threading.Thread.Sleep(new TimeSpan(0,0,6));
                RunTargetCall();
            }
        }

        private static MockCircuitBreakerTarget RunTargetCall()
        {
            var proxy = CircuitBreakerProxyFactory<MockCircuitBreakerTarget>.Create();
            proxy.MakeSureMockDoesNotBreakNextDoWork();
            proxy.DoWork(10);
            return proxy;
        }

        private static MockCircuitBreakerTarget RunFailingTargetCall()
        {
            var proxy = CircuitBreakerProxyFactory<MockCircuitBreakerTarget>.Create();
            proxy.MakeSureMockBreaksNextDoWork();
            proxy.DoWork(10);
            return proxy;
        }

    }

    public class MockCircuitBreakerTarget
    {

        private bool _isInBreakingMode;

        private int _workDoneCount;
        public int WorkDoneCount
        {
            get
            {
                return _workDoneCount;
            } 
        }

        public virtual void DoWork(int i)
        {

            if (_isInBreakingMode) 
                throw new InvalidOperationException("DoWork on MockCircuitBreakerTarget is in breaking mode.");

            _workDoneCount += i;
        }

        public void MakeSureMockBreaksNextDoWork()
        {
            _isInBreakingMode = true;
        }

        public void MakeSureMockDoesNotBreakNextDoWork()
        {
            _isInBreakingMode = false;
        }

    }

}
