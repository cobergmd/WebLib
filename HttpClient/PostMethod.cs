/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.Text;
using System.Web;
using System.Net;

namespace CJO.Web.Http
{
    public class PostMethod : HttpMethod
    {
        private string _CommandText = string.Empty;

        private string _Boundary = ("PostRequestTransport-" + DateTime.Now.ToString("yyyyMMddhHHmmss"));
        private Encoding _CharacterEncoding = Encoding.UTF8;

        public string Boundary
        {
            get
            {
                return _Boundary;
            }
            set
            {
                _Boundary = value;
            }
        }

        public Encoding CharacterEncoding
        {
            get
            {
                return _CharacterEncoding;
            }
            set
            {
                _CharacterEncoding = value;
            }
        }

        public PostMethod()
        {
            base.HttpVerb = "POST";
        }

        public override HttpWebRequest CreateRequest(Uri serviceUri, HttpParameterCollection parameters)
        {
            UriBuilder uriBuilder = new UriBuilder(serviceUri);
            StringBuilder paramStringBuilder = new StringBuilder();
            string key = null;
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    key = parameters[i].Name;
                    paramStringBuilder.AppendFormat("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(parameters[i].Value));
                    if (i < (parameters.Count - 1))
                    {
                        paramStringBuilder.Append("&");
                    }
                }
            }
            uriBuilder.Query = paramStringBuilder.ToString();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);
            request.Method = base.HttpVerb;
            request.ContentType = base.BodyEncoder.ContentType;
            _CommandText = uriBuilder.Uri.AbsoluteUri;
            return request;
        }
    }
}


