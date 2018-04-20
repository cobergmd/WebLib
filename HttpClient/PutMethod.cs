/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.Net;
using System.Text;
using System.Web;

namespace CJO.Web.Http
{
    public class PutMethod : HttpMethod
    {
        private string commandText = string.Empty;

        public PutMethod()
        {
            base.HttpVerb = "PUT";
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
            this.commandText = uriBuilder.Uri.AbsoluteUri;
            return request;
        }
    }
}
