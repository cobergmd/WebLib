/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.IO;
using System.Net;
using System.Text;

namespace CJO.Web.Http
{
    public abstract class HttpMethod : IHttpMethod
    {
        protected string _HttpVerb = "UNKNOWN";
        private HttpBodyEncoder _HttpBodyEncoder = new FormUrlEncoder();
        private bool _SupportsBody = true;

        /// The content type encoding to format request bodies.
        /// Defaults to Form URL encoding (application/x-www-form-urlencoded)
        public HttpBodyEncoder BodyEncoder 
        {
            get
            {
                return _HttpBodyEncoder;
            }
            set
            {
                _HttpBodyEncoder = value;
            }
        }

        public string HttpVerb
        {
            get
            {
                return _HttpVerb;
            }
            protected set
            {
                _HttpVerb = value;
            }
        }

        public bool SupportsBody
        {
            get
            {
                return _SupportsBody;
            }
            set
            {
                _SupportsBody = value;
            }
        }

        protected HttpMethod()
        {
        }

        public virtual HttpWebRequest CreateRequest(Uri serviceUri, HttpParameterCollection parameters)
        {
            return (HttpWebRequest)WebRequest.Create(serviceUri);
        }

        public void WriteRequestBody(Stream stream, HttpClient client, Encoding characterEncoding)
        {
            _HttpBodyEncoder.WriteRequestBody(stream, client, characterEncoding);
        }
    }
}


