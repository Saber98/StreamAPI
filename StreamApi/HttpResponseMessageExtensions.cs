using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace StreamApi
{
    public static class HttpResponseMessageExtensions
    {
        public static T GetFirstHeaderValueOrDefault<T>(
            this HttpResponseMessage response,
            string headerKey)
        {
            var toReturn = default(T);

            IEnumerable<string> headerValues;

            if (response.Content.Headers.TryGetValues(headerKey, out headerValues))
            {
                var valueString = headerValues.FirstOrDefault();
                if (valueString != null)
                {
                    return (T)Convert.ChangeType(valueString, typeof(T));
                }
            }

            return toReturn;
        }
    }
}