using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircuitBreaker;
namespace CircuitBreaker.InjectionPolicy
{
    using System.Dynamic;
    using System.Runtime.Remoting.Messaging;
    using Microsoft.Practices.ObjectBuilder2;
    using Microsoft.Practices.Unity.InterceptionExtension;

    public class CircuitBreakerProxyFactory<T>  where T:class 
     {

        //=> one instance each factory type
        // ReSharper disable once StaticFieldInGenericType
        private static readonly CircuitBreaker circuitBreaker = new CircuitBreaker();

        public static class Settings
        {

            static Settings() { }

            public static TimeSpan HalfOpenWaitTime
            {
                get
                {
                    return circuitBreaker.OpenToHalfOpenWaitTime;
                }
                set
                {
                    circuitBreaker.OpenToHalfOpenWaitTime = value;
                }
            } 
        }

         private CircuitBreakerProxyFactory()
         {
         }

         public static T Create() 
         {
             return Intercept.NewInstance<T>(new VirtualMethodInterceptor(),
                                             new IInterceptionBehavior[] { new CircuitBreakerInterceptionBehavior() });
         }

        private sealed class CircuitBreakerInterceptionBehavior:  IInterceptionBehavior
        {
            public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
            {                
           
                IMethodReturn returnValue = null; 
                Action baseaction = () =>
                {
                    returnValue = getNext()(input, getNext);
                    if (returnValue.Exception != null)
                            throw returnValue.Exception;
                };
                
                circuitBreaker.ExecuteAction(baseaction);
                return returnValue;
 
            }

            public IEnumerable<Type> GetRequiredInterfaces()
            {
                return Type.EmptyTypes;
            }

            public bool WillExecute
            {
                get
                {
                    return true;
                }  
            }
        }         
     }
} 
 