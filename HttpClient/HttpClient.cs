/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace CJO.Web.Http
{
    public class HttpClient
    {
        private CookieCollection _Coookies;
        private HttpDataSourceCollection _Data;
        private WebHeaderCollection _Headers;
        private HttpParameterCollection _Parameters;
        private int _RedirectCount;
        private int _RedirectLimit;

        public bool AllowAutoRedirect { get; set; }
        public ICredentials Credentials { get; set; }
        public object Object { get; set; }
        public Type ObjectType { get; set; }

        public CookieCollection Cookies
        {
            get
            {
                return ((_Coookies == null) ? (_Coookies = new CookieCollection()) : _Coookies);
            }
        }

        public HttpDataSourceCollection Data
        {
            get
            {
                return ((_Data == null) ? (_Data = new HttpDataSourceCollection()) : _Data);
            }
        }

        public WebHeaderCollection Headers
        {
            get
            {
                return ((_Headers == null) ? (_Headers = new WebHeaderCollection()) : _Headers);
            }
        }

        public HttpParameterCollection Parameters
        {
            get
            {
                return ((_Parameters == null) ? (_Parameters = new HttpParameterCollection()) : _Parameters);
            }
            set
            {
                this._Parameters = value;
            }
        }

        public int RedirectLimit
        {
            get
            {
                return _RedirectLimit;
            }
            set
            {
                _RedirectLimit = Math.Abs(value);
            }
        }

        public Uri ServiceUri { get; set; }

        public TimeSpan Timeout { get; set; }

        public HttpMethod Method { get; set; }

        /// Raised whenever the command encounters Response headers requiring
        /// redirect.
        /// TODO: Implement event args
        public delegate void RedirectingHandler(object sender, HttpMethodEventArgs e);
        
        public event RedirectingHandler Redirecting;

        public HttpClient()
        {
            AllowAutoRedirect = true;
            _RedirectCount = 0;
            _RedirectLimit = 10;
            Timeout = new TimeSpan(0, 0, 0, 30, 0);
        }

        public HttpClient(string serviceUrl)
        {
            AllowAutoRedirect = true;
            _RedirectCount = 0;
            _RedirectLimit = 10;
            Timeout = new TimeSpan(0, 0, 0, 30, 0);
            ServiceUri = new Uri(serviceUrl);
        }

        public HttpClient(Uri serviceUri)
        {
            AllowAutoRedirect = true;
            _RedirectCount = 0;
            _RedirectLimit = 10;
            Timeout = new TimeSpan(0, 0, 0, 30, 0);
            ServiceUri = serviceUri;
        }

        public HttpClient(string serviceUrl, HttpMethod method)
            : this(serviceUrl)
        {
            this.Method = method;
        }

        public HttpClient(Uri serviceUri, HttpMethod method)
            : this(serviceUri)
        {
            Method = method;
        }

        public HttpWebResponse Execute()
        {
            if (null == ServiceUri)
            {
                throw new InvalidOperationException("Required ServiceUri is missing");
            }

            HttpWebRequest request = Method.CreateRequest(ServiceUri, Parameters);
            if (Credentials != null)
            {
                request.Credentials = Credentials;
            }
            request.Timeout = Convert.ToInt32(this.Timeout.TotalMilliseconds);
            request.AllowAutoRedirect = this.AllowAutoRedirect;
            request.Method = this.Method.HttpVerb;
            request.ContentType = this.Method.BodyEncoder.ContentType;
            if ((this.Headers != null) && (this.Headers.Count > 0))
            {
                request.Headers.Add(this.Headers);
            }
            if ((this.Cookies != null) && (this.Cookies.Count > 0))
            {
                request.CookieContainer = new CookieContainer(this.Cookies.Count);
                request.CookieContainer.Add(this.Cookies);
            }
            if (this.Method.SupportsBody)
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    this.Method.WriteRequestBody(memStream, this, Encoding.UTF8);
                    request.ContentLength = memStream.Length;
                    memStream.WriteTo(request.GetRequestStream());
                }
            }
            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
                if (((httpResponse.StatusCode == HttpStatusCode.Found) || (httpResponse.StatusCode == HttpStatusCode.Found)) || (httpResponse.StatusCode == HttpStatusCode.TemporaryRedirect))
                {
                    return this.Redirect(httpResponse);
                }
                return httpResponse;
            }
            catch (WebException ex)
            {
                return (HttpWebResponse)ex.Response;
            }
        }

        public StreamReader ExecuteStreamReader()
        {
            return new StreamReader(this.GetResponseStream());
        }

        public Stream GetResponseStream()
        {
            try
            {
                return this.Execute().GetResponseStream();
            }
            catch (WebException ex)
            {
                return ex.Response.GetResponseStream();
            }
        }

        protected void OnRedirecting(HttpWebResponse response)
        {
            if (this.Redirecting != null)
            {
                this.Redirecting(this, new HttpMethodEventArgs(response));
            }
        }

        public HttpWebResponse Redirect(HttpWebResponse httpResponse)
        {
            if (null == httpResponse.Headers["Location"])
            {
                throw new HttpClientException("Unable to redirect because there was no \"Location\" header received");
            }
            if (_RedirectCount++ > this.RedirectLimit)
            {
                throw new HttpClientException("Encountered too many redirects");
            }
            this.ServiceUri = new Uri(httpResponse.Headers["Location"]);
            return this.Execute();
        }
    }
}


