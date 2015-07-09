namespace Recruiting.ApplicationServices
{
    using System;
    using Recruiting.Domain;

    public class ResultValidation<T>
    {
        private readonly T result;

        public ResultValidation(T result)
        {
            this.result = result;
        }

        public bool Succeeded
        {
            get
            {
                return this.result != null;
            }
        }

        public T Result
        {
            get
            {
                return this.result;
            }
        }
    }
}
