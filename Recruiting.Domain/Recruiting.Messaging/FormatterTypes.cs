using System;
using System.Collections.Generic;
using System.Linq;
using Recruiting.Domain;
using Recruiting.Domain.Infrastructure.Messaging;

namespace Recruiting.Messaging
{
    public static class FormatterTypes
    {
        private static IEnumerable<Type> _formatterTypes;

        public static IEnumerable<Type> GetFormatterTypes()
        {
            if (_formatterTypes == null)
            {
                _formatterTypes =
                    typeof(ScreeningCreated).Assembly
                        .GetTypes()
                        .Where(x => typeof(IMessage).IsAssignableFrom(x))
                        .Where(x => !x.IsAbstract)
                        .Where(x => x.IsClass);
            }

            return _formatterTypes;

        }
    }
}