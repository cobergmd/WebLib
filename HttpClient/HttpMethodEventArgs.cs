/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.Net;

namespace CJO.Web.Http
{
    public class HttpMethodEventArgs : EventArgs
    {
        private HttpWebResponse _Response;

        public HttpMethodEventArgs()
        {
        }

        public HttpMethodEventArgs(HttpWebResponse response)
            : this()
        {
            _Response = response;
        }

        public HttpWebResponse Response
        {
            get
            {
                return _Response;
            }
            set
            {
                _Response = value;
            }
        }
    }
}


