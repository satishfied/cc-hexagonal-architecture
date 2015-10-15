using System;
using System.Collections.Generic;
using System.Net;
using DDDSkeleton.ApplicationServices.Services;

namespace DDDSkeleton.WebService.Helpers
{
    public static class ExceptionDictionary
    {
        public static HttpStatusCode ConvertToHttpStatusCode(this Exception exception)
        {
            var dictionary = GetExceptionDictionary();
            return dictionary.ContainsKey(exception.GetType())
                ? dictionary[exception.GetType()]
                : dictionary[typeof (Exception)];
        }

        private static Dictionary<Type, HttpStatusCode> GetExceptionDictionary()
        {
            var dict = new Dictionary<Type, HttpStatusCode>();
            dict[typeof (ResourceNotFoundException)] = HttpStatusCode.NotFound;
            dict[typeof (Exception)] = HttpStatusCode.InternalServerError;

            return dict;
        }
    }
}