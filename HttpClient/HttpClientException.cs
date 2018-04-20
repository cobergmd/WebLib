/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;

namespace CJO.Web.Http
{
    public class HttpClientException : ApplicationException
    {
        public HttpClientException(string message)
            : base(message)
        {
        }

        public HttpClientException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}


